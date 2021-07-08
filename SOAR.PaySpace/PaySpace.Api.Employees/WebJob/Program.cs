#region NameSpace
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoapHttpClient;
using SoapHttpClient.Enums;
using SOARIntegration.PaySpace.Api.Employees.Repository;
using SOARIntegration.PaySpace.Api.Employees.Service;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace SOARIntegration.PaySpace.Api.Employees.WebJob
{
	public class Program
	{
		public static void Main()
		{
			var sharedFolder = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "SOAR.Shared");
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();

			//Configure services
			IConfigurationRoot configuration = builder.Build();

			var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
			var userName = Environment.GetEnvironmentVariable("PaySpaceUserName");
			var password = Environment.GetEnvironmentVariable("PaySpacePassword");
			var url = Environment.GetEnvironmentVariable("PaySpaceUri");
			var payspaceSubUrl = Environment.GetEnvironmentVariable("payspaceSubUrl");
			var serviceProvider = new ServiceCollection()
				.AddLogging()
				.AddDbContext<Context>(options => options.UseSqlServer(connectionString))
				.AddScoped(typeof(IRepository<>), typeof(Repository<>))
				.AddSingleton<IEmployeesService, EmployeeService>()
				.BuildServiceProvider();

			var employeeService = serviceProvider.GetService<IEmployeesService>();
			Console.WriteLine("Payspace url : " + url);
			Console.WriteLine("Payspace SubURL : " + payspaceSubUrl);
			Console.WriteLine("Fetching employee data start at " + DateTime.Now);
			Console.WriteLine("\n ------------------------------------------------");
			EmployeesData objEmployeesData = new EmployeesData(employeeService);
			objEmployeesData.ProcessData(userName, password, url, payspaceSubUrl);
		}
	}

	public partial class EmployeesData
	{
		#region Private Fields
		private readonly IEmployeesService _employeesService;
		#endregion

		#region CTOR
		public EmployeesData(IEmployeesService employeesService)
		{
			this._employeesService = employeesService;
		}
		#endregion

		#region ProcessData
		public void ProcessData(string userName, string password, string url, string payspaceSubUrl)
		{
			try
			{
				EmployeeModel objEmployeeModel = new EmployeeModel();
				var lastBatchId = objEmployeeModel.GetLastBatchId(this._employeesService);

				var bchId = lastBatchId + 1;
				Console.WriteLine("Payspace service call started for batch " + bchId + " at " + DateTime.Now);
				var soapClient = new SoapClient();
				XNamespace nameSpace = "http://www.payspace.co.za/webservices/";
				var result = soapClient.Post(
						new Uri(url + payspaceSubUrl),
						SoapVersion.Soap12,
						body: new XElement(nameSpace + "EmployeeChanges",
										new XElement(nameSpace + "UserName", userName),
										new XElement(nameSpace + "Password", password),
										new XElement(nameSpace + "LastBatch", lastBatchId + 1),
										new XElement(nameSpace + "GenerateOnly", "false")
								)
							);

				Task<Stream> streamTask = result.Content.ReadAsStreamAsync();
				Stream stream = streamTask.Result;
				var sr = new StreamReader(stream);
				var soapResponse = XDocument.Load(sr);

				IEnumerable<XElement> list = soapResponse.Document.Descendants("row");

				Console.WriteLine("Payspace service returned records : " + list.ToList().Count() + " at " + DateTime.Now);
				if (list.ToList().Count() > 0)
				{
					List<Employee> employees = new List<Employee>();
					foreach (var item in list.ToList())
					{
						Employee objEmployee = new Employee();
						objEmployee.EntityId = item.Attribute("empid") != null ? item.Attribute("empid").Value : string.Empty;
						objEmployee.CompanyID = item.Attribute("compid") != null ? item.Attribute("compid").Value : string.Empty;
						objEmployee.CompanyName = item.Attribute("Company") != null ? item.Attribute("Company").Value : string.Empty;
						objEmployee.FirstName = item.Attribute("FName") != null ? item.Attribute("FName").Value : string.Empty;
						objEmployee.LastName = item.Attribute("LName") != null ? item.Attribute("LName").Value : string.Empty;
						objEmployee.EmpNo = item.Attribute("EmpNo") != null ? item.Attribute("EmpNo").Value : string.Empty;
						objEmployee.Email = item.Attribute("Email") != null ? item.Attribute("Email").Value : string.Empty;
						objEmployee.EmploymentDate = item.Attribute("EDate") != null && !string.IsNullOrEmpty(item.Attribute("EDate").Value) ? System.Convert.ToDateTime(item.Attribute("EDate").Value) : (DateTime?)null;
						objEmployee.GroupJoinDate = item.Attribute("GDate") != null && !string.IsNullOrEmpty(item.Attribute("GDate").Value) ? System.Convert.ToDateTime(item.Attribute("GDate").Value) : (DateTime?)null;
						objEmployee.FirstCustomDate = item.Attribute("FDate") != null && !string.IsNullOrEmpty(item.Attribute("FDate").Value) ? System.Convert.ToDateTime(item.Attribute("FDate").Value) : (DateTime?)null;
						objEmployee.SecondCustomDate = item.Attribute("SDate") != null && !string.IsNullOrEmpty(item.Attribute("SDate").Value) ? System.Convert.ToDateTime(item.Attribute("SDate").Value) : (DateTime?)null;
						objEmployee.DateOfBirth = item.Attribute("BDate") != null && !string.IsNullOrEmpty(item.Attribute("BDate").Value) ? System.Convert.ToDateTime(item.Attribute("BDate").Value) : (DateTime?)null;
						objEmployee.Phone = item.Attribute("CNumber") != null ? item.Attribute("CNumber").Value : string.Empty;
						objEmployee.FullName = item.Attribute("FullName") != null ? item.Attribute("FullName").Value : string.Empty;
						objEmployee.Grade = item.Attribute("Grade") != null ? item.Attribute("Grade").Value : string.Empty;

						objEmployee.Project = item.Attribute("Project") != null ? item.Attribute("Project").Value : string.Empty;
						objEmployee.IDNumber = item.Attribute("IDNo") != null ? item.Attribute("IDNo").Value : string.Empty;
						objEmployee.Picture = item.Attribute("Image") != null ? item.Attribute("Image").Value : string.Empty;
						objEmployee.Initials = item.Attribute("Init") != null ? item.Attribute("Init").Value : string.Empty;

						objEmployee.ManagerEmployeeNumber = item.Attribute("ManENumber") != null ? item.Attribute("ManENumber").Value : string.Empty;
						objEmployee.ManagerName = item.Attribute("ManName") != null ? item.Attribute("ManName").Value : string.Empty;
						objEmployee.OnPensionOrPF = item.Attribute("OnPension") != null ? item.Attribute("OnPension").Value : string.Empty;
						objEmployee.OrgUnit = item.Attribute("OrgUnit") != null ? item.Attribute("OrgUnit").Value : string.Empty;

						objEmployee.OrgUnits = item.Attribute("OrgUnits") != null ? item.Attribute("OrgUnits").Value : null;
						objEmployee.Position = item.Attribute("Position") != null ? item.Attribute("Position").Value : string.Empty;

						objEmployee.PositionCode = item.Attribute("PosCode") != null ? item.Attribute("PosCode").Value : string.Empty;
						objEmployee.PayPoint = item.Attribute("PPoint") != null ? item.Attribute("PPoint").Value : string.Empty;
						objEmployee.RegLoc = item.Attribute("RegLoc") != null ? item.Attribute("RegLoc").Value : string.Empty;

						DateTime terminationDate = new DateTime();
						objEmployee.TerminationDate = item.Attribute("TDate") != null ? System.DateTime.TryParse(item.Attribute("TDate").Value, out terminationDate) ? terminationDate : (DateTime?)null : (DateTime?)null;
						objEmployee.Title = item.Attribute("Title") != null ? item.Attribute("Title").Value : string.Empty;
						objEmployee.WorkNumber = item.Attribute("WNumber") != null ? item.Attribute("WNumber").Value : string.Empty;
						objEmployee.FreqName = item.Attribute("FreqName") != null ? item.Attribute("FreqName").Value : string.Empty;
						objEmployee.JobTitle = item.Attribute("Job") != null ? item.Attribute("Job").Value : string.Empty;
						objEmployee.JobCode = item.Attribute("JobCode") != null ? item.Attribute("JobCode").Value : string.Empty;

						objEmployee.Roster = item.Attribute("Roster") != null ? item.Attribute("Roster").Value : string.Empty;
						objEmployee.ActionTypeCode = item.Attribute("ActCode") != null ? item.Attribute("ActCode").Value : string.Empty;
						objEmployee.ActionReasonCode = item.Attribute("ActReaCode") != null ? item.Attribute("ActReaCode").Value : string.Empty;

						DateTime actionEffectiveDate = new DateTime();
						objEmployee.ActionEffectiveDate = item.Attribute("ActEffDate") != null ? System.DateTime.TryParse(item.Attribute("ActEffDate").Value, out actionEffectiveDate) ? actionEffectiveDate : (DateTime?)null : (DateTime?)null;
						objEmployee.RecordAction = item.Attribute("RAct") != null ? item.Attribute("RAct").Value : string.Empty;

						objEmployee.HomeNumber = item.Attribute("HNumber") != null ? item.Attribute("HNumber").Value : string.Empty;
						objEmployee.Gender = item.Attribute("Gender") != null ? item.Attribute("Gender").Value : string.Empty;
						objEmployee.ManagerRegion = item.Attribute("ManRegion") != null ? item.Attribute("ManRegion").Value : string.Empty;
						objEmployee.ManagerJobCode = item.Attribute("ManJobCode") != null ? item.Attribute("ManJobCode").Value : string.Empty;
						objEmployee.Custom = item.Attribute("Custom") != null ? item.Attribute("Custom").Value : string.Empty;
						objEmployee.Customers = item.Attribute("Customers") != null ? item.Attribute("Customers").Value : string.Empty;
						objEmployee.CustomersCode = item.Attribute("CustomersCode") != null ? item.Attribute("CustomersCode").Value : string.Empty;
						objEmployee.BatchId = lastBatchId + 1;

						employees.Add(objEmployee);
					}

					Console.WriteLine("Employee Data insertion started " + " at " + DateTime.Now);

					EmployeeModel objEmployeeModels = new EmployeeModel(employees);
					objEmployeeModels.ProcessData(this._employeesService);

					Console.WriteLine("Employee Data insertion finished " + " at " + DateTime.Now);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + "\\n" + ex.StackTrace);
			}
		}
		#endregion
	}
}
