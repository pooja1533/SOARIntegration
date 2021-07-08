using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_OverpaymentAllocation")]
	public class OverpaymentAllocation
	{
		public Guid OverpaymentId { get; set; }
		public int AllocationOrder { get; set; }
		public Guid? InvoiceId { get; set; }
		public decimal? AppliedAmount { get; set; }
		public DateTime? Date { get; set; }
		public decimal? Amount { get; set; }
	}
}
