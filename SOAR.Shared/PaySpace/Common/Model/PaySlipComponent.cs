using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.PaySpace.Common.Model
{
	[Table("tbl_SOAR_PaySpace_EmployeePayslipDetails_Component")]
	public class PaySlipComponent : BaseEntity
	{
		public string CompanyCode { get; set; }
		public string ComponentCode { get; set; }
		public string ComponentDescription { get; set; }
		public string AlternateComponentDescription { get; set; }
		public string PayslipAction { get; set; }
		public string PaySlipDate { get; set; }
		public string TaxCode { get; set; }
		public string ComponentValue { get; set; }
		public string Comments { get; set; }
		public string Quantity { get; set; }
		public string Frequency { get; set; }		
	}
}
