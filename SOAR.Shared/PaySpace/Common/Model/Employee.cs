using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.PaySpace.Common.Model
{
	[Table("tbl_SOAR_PaySpace_Employee")]
	public class Employee : BaseEntity
	{
		public string CompanyID { get; set; }
		public string CompanyName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmpNo { get; set; }
		public string Email { get; set; }
		public DateTime? EmploymentDate { get; set; }
		public DateTime? GroupJoinDate { get; set; }
		public DateTime? FirstCustomDate { get; set; }
		public DateTime? SecondCustomDate { get; set; }
		public string Phone { get; set; }
		public string FullName { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string Project { get; set; }
		public string Grade { get; set; }
		public string IDNumber { get; set; }
		public string Picture { get; set; }
		public string Initials { get; set; }
		public string ManagerEmployeeNumber { get; set; }
		public string ManagerName { get; set; }
		public string OnPensionOrPF { get; set; }
		public string OrgUnit { get; set; }
		public string OrgUnits { get; set; }
		public string Position { get; set; }
		public string PositionCode { get; set; }
		public string PayPoint { get; set; }
		public string RegLoc { get; set; }
		public DateTime? TerminationDate { get; set; }
		public string Title { get; set; }
		public string WorkNumber { get; set; }
		public string FreqName { get; set; }
		public string JobTitle { get; set; }
		public string JobCode { get; set; }
		public string Roster { get; set; }
		public string ActionTypeCode { get; set; }
		public string ActionReasonCode { get; set; }
		public DateTime? ActionEffectiveDate { get; set; }
		public string RecordAction { get; set; }
		public string HomeNumber { get; set; }
		public string Gender { get; set; }
		public string ManagerRegion { get; set; }
		public string ManagerJobCode { get; set; }
		public string Custom { get; set; }
		public string Customers { get; set; }
		public string CustomersCode { get; set; }
		public int BatchId { get; set; }
	}
}
