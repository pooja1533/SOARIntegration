using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.PaySpace.Common.Model
{
	[Table("tbl_SOAR_PaySpace_EmployeePayslipDetails_Leave")]
	public class PaySlipLeave : BaseEntity
	{
		public string LeaveDescription { get; set; }
		public double? LeaveBalance { get; set; }
		public double? LeaveAccrual { get; set; }
		public double? LeaveForfeit { get; set; }
		public double? DaysTaken { get; set; }
		public string CompanyCode { get; set; }
		public string PaySlipDate { get; set; }
	}
}
