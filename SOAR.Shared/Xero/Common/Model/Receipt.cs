using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_Xero_Receipt")]
	public class Receipt
	{
		public Guid Id { get; set; }
		public DateTime? Date { get; set; }
		public Guid? ContactId { get; set; }
		public Guid? UserId { get; set; }
		public string Reference { get; set; }
		public string LineAmountTypes { get; set; }
		public decimal? SubTotal { get; set; }
		public decimal? TotalTax { get; set; }
		public decimal? Total { get; set; }
		public string Status { get; set; }
		public string ReceiptNumber { get; set; }
		public bool? HasAttachments { get; set; }
		public string Url { get; set; }
		public DateTime? UpdatedDateUtc { get; set; }
	}
}