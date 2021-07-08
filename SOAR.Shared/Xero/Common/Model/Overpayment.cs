using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_Overpayment")]
	public class Overpayment
	{
		public Guid Id { get; set; }
		public Guid? ReferenceId { get; set; }
		public bool? HasAttachments { get; set; }
		public decimal RemainingCredit { get; set; }
		public decimal? AppliedAmount { get; set; }
		public string Type { get; set; }
		public decimal? CurrencyRate { get; set; }
		public string CurrencyCode { get; set; }
		public decimal? Total { get; set; }
		public decimal? TotalTax { get; set; }
		public decimal? SubTotal { get; set; }
		public string LineAmountTypes { get; set; }
		public string Status { get; set; }
		public DateTime? Date { get; set; }
		public Guid? ContactId { get; set; }
		public DateTime? UpdatedDateUtc { get; set; }
	}
}
