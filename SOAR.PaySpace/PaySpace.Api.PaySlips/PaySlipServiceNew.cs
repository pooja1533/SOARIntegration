using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SoapHttpClient;
using SoapHttpClient.Enums;
using SOARIntegration.PaySpace.Api.PaySlips.Repository;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PaySpace.Api.PaySlips
{
	public class PaySlipServiceNew
	{
		#region Local Variables
		private readonly ILogger c_log;
		private readonly Context c_context;

		#endregion

		#region Constructor
		public PaySlipServiceNew(ILogger<PaySlipServiceNew> log, Context context)
		{
			c_log = log;
			c_context = context;
			c_context.Database.SetCommandTimeout(3600);
		}
		#endregion

		#region Public Methods
		public void Import()
		{
			Task.Run(() => this.ImportPaySlipComponent()).Wait();
		}
		#endregion

		#region Private Methods
		private async Task ImportPaySlipComponent()
		{
			//#region For Development Uncomment this
			//var payspaceUserName = "webservice@a1l.co.za";
			//var payspacePassword = "@1Lweb7896";
			//var payspaceUri = "https://classicapi.payspace.com";
			//var payspaceCompany = "A1LH,A1LI,A1LR,IC,A1LRH,A1LRI,A1LD";
			//var payspaceCompany = "A1LH";
			//var payslipYear = "2020";
			//#endregion

			#region During Development comment this
			var payspaceUserName = Environment.GetEnvironmentVariable("PaySpaceUserName");
			var payspacePassword = Environment.GetEnvironmentVariable("PaySpacePassword");
			var payspaceUri = Environment.GetEnvironmentVariable("PaySpaceUri");
			var payspaceSubUrl = Environment.GetEnvironmentVariable("payspaceSubUrl");
			var payspaceCompany = Environment.GetEnvironmentVariable("PaySpaceCompany");
			var payslipYear = Environment.GetEnvironmentVariable("PaySlipYear");
			#endregion

			try
			{
				var companyList = payspaceCompany.Split(',');
				Console.WriteLine("Payspace url : " + payspaceUri);
				Console.WriteLine("Payspace SubURL : " + payspaceSubUrl);

				for (var companyCount = 0; companyCount < companyList.Count(); companyCount++)
				{
					var payslipYearNew = System.Convert.ToInt32(payslipYear);
					var monthDifference = 13; // 13 is set so we can check for next year data.

					for (var payslipMonth = 1; payslipMonth <= monthDifference; payslipMonth++)
					{
						List<PaySlipLeave> paySlipLeaves = new List<PaySlipLeave>();
						List<PaySlipComponent> paySlipComponents = new List<PaySlipComponent>();

						if (payslipMonth > 12 && payslipYearNew < DateTime.Now.Year)
						{
							payslipMonth = 1;
							payslipYearNew = payslipYearNew + 1;
							monthDifference = 13;
						}
						else if (payslipMonth > 12 && payslipYearNew == DateTime.Now.Year)
						{
							payslipMonth = 1;
							payslipYearNew = payslipYearNew + 1;
							monthDifference = DateTime.Now.Month - 1;
						}

						Console.WriteLine("Fetching data for month : " + payslipMonth + " and year : " + payslipYearNew + " and company : " + companyList[companyCount]);

						if (payslipMonth <= 12)
						{

							IEnumerable<XElement> employees = null;
							try
							{
								Console.WriteLine("Payspace service call started for company " + companyList[companyCount] + " year " + payslipYearNew + " month " + payslipMonth + " at " + DateTime.Now);
								var soapClient = new SoapClient();
								XNamespace myns = "http://www.payspace.com/webservices/";
								var result = soapClient.Post(
										new Uri(payspaceUri + payspaceSubUrl),
										SoapVersion.Soap12,
										body: new XElement(myns + "PayslipDetails",
														new XElement(myns + "UserName", payspaceUserName),
														new XElement(myns + "Password", payspacePassword),
														new XElement(myns + "XMLString", "<Emp><ComCode>" + companyList[companyCount] + "</ComCode><Year>" + payslipYearNew + "</Year><Month>" + payslipMonth + "</Month><PerRun>Yes</PerRun></Emp>"),
														new XElement(myns + "EncryptedPacket", "true")
												),
										action: "http://www.payspace.com/webservices/PayslipDetails"
											);

								Task<Stream> streamTask = result.Content.ReadAsStreamAsync();
								Stream stream = streamTask.Result;
								var sr = new StreamReader(stream);
								var soapResponse = XDocument.Load(sr);

								employees = soapResponse.Document.Descendants("Emp");
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}

							if (employees != null)
							{
								Console.WriteLine("Payspace service returned records : " + employees.ToList().Count + ". For company " + companyList[companyCount] + " year " + payslipYearNew + " month " + payslipMonth + " at " + DateTime.Now);

								for (int count = 0; count < employees.ToList().Count; count++)
								{
									// Fetch leave types records.
									IEnumerable<XElement> leaveTypes = employees.ToList()[count].Descendants("LveTpe");
									foreach (var leaveType in leaveTypes)
									{
										PaySlipLeave objPayslipLeave = new PaySlipLeave();
										objPayslipLeave.EntityId = employees.ToList()[count].Attribute("EmpNo").Value;
										objPayslipLeave.LeaveDescription = leaveType.Attribute("LveBckt").Value;
										objPayslipLeave.LeaveBalance = System.Convert.ToDouble(leaveType.Attribute("LveBal").Value);
										objPayslipLeave.LeaveAccrual = System.Convert.ToDouble(leaveType.Attribute("LveAccr").Value);
										objPayslipLeave.LeaveForfeit = System.Convert.ToDouble(leaveType.Attribute("LveFrft").Value);
										objPayslipLeave.DaysTaken = System.Convert.ToDouble(leaveType.Attribute("DaysTkn").Value);
										objPayslipLeave.CompanyCode = companyList[companyCount];
										objPayslipLeave.PaySlipDate = employees.ToList()[count].Attribute("RunD").Value;
										objPayslipLeave.Audit_Created = DateTime.UtcNow;
										objPayslipLeave.Audit_Modified = DateTime.UtcNow;
										objPayslipLeave.Audit_Deleted = false;

										paySlipLeaves.Add(objPayslipLeave);
									}

									// Fetch payslip data.
									IEnumerable<XElement> components = employees.ToList()[count].Descendants("Com");
									foreach (var component in components.ToList())
									{
										PaySlipComponent objPaySlipComponent = new PaySlipComponent();
										objPaySlipComponent.EntityId = employees.ToList()[count].Attribute("EmpNo").Value;
										objPaySlipComponent.CompanyCode = companyList[companyCount];
										objPaySlipComponent.ComponentCode = component.Attribute("ComCd").Value;
										objPaySlipComponent.ComponentDescription = component.Attribute("ComDes").Value;
										objPaySlipComponent.AlternateComponentDescription = component.Attribute("ComADes").Value;
										objPaySlipComponent.PayslipAction = component.Attribute("PA").Value;
										objPaySlipComponent.PaySlipDate = employees.ToList()[count].Attribute("RunD").Value;
										objPaySlipComponent.TaxCode = component.Attribute("TCde").Value;
										objPaySlipComponent.ComponentValue = component.Attribute("CVle").Value;
										objPaySlipComponent.Comments = component.Attribute("Comm").Value;
										objPaySlipComponent.Quantity = component.Attribute("Qty").Value;
										objPaySlipComponent.Frequency = component.Attribute("Freq").Value;
										objPaySlipComponent.Audit_Created = DateTime.UtcNow;
										objPaySlipComponent.Audit_Modified = DateTime.UtcNow;
										objPaySlipComponent.Audit_Deleted = false;
										paySlipComponents.Add(objPaySlipComponent);
									}
								}

								await ProcessPaySlipComponents(companyList[companyCount], paySlipComponents);
								await ProcessPaySlipLeaves(companyList[companyCount], paySlipLeaves);
							}
						}
					}
				}				
			}
			catch (Exception ex)
			{
				Console.WriteLine("error : " + ex.Message);
			}
		}

		private async Task ProcessPaySlipLeaves(string payspaceCompany, List<PaySlipLeave> paySlipLeaves)
		{
			try
			{
				DataTable payslipLeaveDataTable = new DataTable();
				payslipLeaveDataTable.Columns.AddRange(new DataColumn[11]
				{
					new DataColumn("EntityId", typeof(string)),
					new DataColumn("CompanyCode", typeof(string)),
					new DataColumn("PaySlipDate", typeof(string)),
					new DataColumn("LeaveDescription", typeof(string)),
					new DataColumn("LeaveBalance", typeof(float)),
					new DataColumn("LeaveAccrual", typeof(float)),
					new DataColumn("LeaveForfeit",typeof(float)),
					new DataColumn("DaysTaken", typeof(float)),
					new DataColumn("Audit_IsDeleted", typeof(bool)),
					new DataColumn("Audit_Created", typeof(DateTime)),
					new DataColumn("Audit_Modified", typeof(DateTime))
				});



				for (var paySlipLeaveCount = 0; paySlipLeaveCount < paySlipLeaves.Count; paySlipLeaveCount++)
				{
					payslipLeaveDataTable.Rows.Add(paySlipLeaves[paySlipLeaveCount].EntityId, paySlipLeaves[paySlipLeaveCount].CompanyCode,
						paySlipLeaves[paySlipLeaveCount].PaySlipDate, paySlipLeaves[paySlipLeaveCount].LeaveDescription,
						paySlipLeaves[paySlipLeaveCount].LeaveBalance, paySlipLeaves[paySlipLeaveCount].LeaveAccrual,
						paySlipLeaves[paySlipLeaveCount].LeaveForfeit,
						paySlipLeaves[paySlipLeaveCount].DaysTaken,
						paySlipLeaves[paySlipLeaveCount].Audit_Deleted, paySlipLeaves[paySlipLeaveCount].Audit_Created, paySlipLeaves[paySlipLeaveCount].Audit_Modified);
				}

				var paySlipDetails = new SqlParameter("List", SqlDbType.Structured);
				paySlipDetails.Value = payslipLeaveDataTable;
				paySlipDetails.TypeName = "tbl_SOAR_PaySpace_EmployeePayslipDetails_LeaveTbl";

				if (paySlipLeaves.Count > 0)
				{
					Console.WriteLine("Payslip leaves Data insertion start for company " + paySlipLeaves[0].CompanyCode + " payslip " + paySlipLeaves[0].PaySlipDate + " at " + DateTime.Now);
				}
				await c_context.Database.ExecuteSqlCommandAsync("InsertDetailsOfPaySlipLeave @List;", paySlipDetails);
				if (paySlipLeaves.Count > 0)
				{
					Console.WriteLine("Payslip leaves Data insertion end for company " + paySlipLeaves[0].CompanyCode + " payslip " + paySlipLeaves[0].PaySlipDate + " at " + DateTime.Now);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("error : " + ex.Message);
				throw ex;
			}
		}

		private async Task ProcessPaySlipComponents(string payspaceCompany, List<PaySlipComponent> paySlipComponents)
		{
			try
			{
				DataTable payslipDataTable = new DataTable();
				payslipDataTable.Columns.AddRange(new DataColumn[15]
				{
					new DataColumn("EntityId", typeof(string)),
					new DataColumn("CompanyCode", typeof(string)),
					new DataColumn("ComponentCode", typeof(string)),
					new DataColumn("ComponentDescription", typeof(string)),
					new DataColumn("AlternateComponentDescription", typeof(string)),
					new DataColumn("PayslipAction", typeof(string)),
					new DataColumn("PaySlipDate", typeof(string)),
					new DataColumn("TaxCode", typeof(string)),
					new DataColumn("ComponentValue", typeof(string)),
					new DataColumn("Comments", typeof(string)),
					new DataColumn("Quantity", typeof(string)),
					new DataColumn("Frequency", typeof(string)),
					new DataColumn("Audit_IsDeleted", typeof(bool)),
					new DataColumn("Audit_Created", typeof(DateTime)),
					new DataColumn("Audit_Modified", typeof(DateTime))
				});

				for (var paySlipCount = 0; paySlipCount < paySlipComponents.Count; paySlipCount++)
				{
					payslipDataTable.Rows.Add(paySlipComponents[paySlipCount].EntityId, paySlipComponents[paySlipCount].CompanyCode,
						paySlipComponents[paySlipCount].ComponentCode, paySlipComponents[paySlipCount].ComponentDescription,
						paySlipComponents[paySlipCount].AlternateComponentDescription, paySlipComponents[paySlipCount].PayslipAction,
						paySlipComponents[paySlipCount].PaySlipDate, paySlipComponents[paySlipCount].TaxCode, paySlipComponents[paySlipCount].ComponentValue,
						paySlipComponents[paySlipCount].Comments, paySlipComponents[paySlipCount].Quantity, paySlipComponents[paySlipCount].Frequency,
						paySlipComponents[paySlipCount].Audit_Deleted, paySlipComponents[paySlipCount].Audit_Created, paySlipComponents[paySlipCount].Audit_Modified);
				}

				var paySlipDetails = new SqlParameter("List", SqlDbType.Structured);
				paySlipDetails.Value = payslipDataTable;
				paySlipDetails.TypeName = "tbl_SOAR_PaySpace_EmployeePayslipDetails_ComponentTbl";

				if (paySlipComponents.Count > 0)
				{
					Console.WriteLine("PaySlipComponent Data insertion start for company " + paySlipComponents[0].CompanyCode + " payslip " + paySlipComponents[0].PaySlipDate + " at " + DateTime.Now);
				}

				await c_context.Database.ExecuteSqlCommandAsync("InsertDetailsOfPaySlipComponent @List;", paySlipDetails);

				if (paySlipComponents.Count > 0)
				{
					Console.WriteLine("PaySlipComponent Data insertion end for company " + paySlipComponents[0].CompanyCode + " payslip " + paySlipComponents[0].PaySlipDate + " at " + DateTime.Now);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("error : " + ex.Message);
				throw ex;
			}
		}

		private string GetMonthName(string month)
		{
			switch (month)
			{
				case "1":
					return "January";
				case "2":
					return "February";
				case "3":
					return "March";
				case "4":
					return "April";
				case "5":
					return "May";
				case "6":
					return "June";
				case "7":
					return "July";
				case "8":
					return "August";
				case "9":
					return "September";
				case "10":
					return "October";
				case "11":
					return "November";
				case "12":
					return "December";
				default:
					return string.Empty;
			}
		}
		#endregion
	}

}
