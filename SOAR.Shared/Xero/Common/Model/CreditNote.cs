using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_CreditNote")]
	public class CreditNote
	{
		public Guid Id { get; set; }
		public Guid ReferenceId { get; set; }
		public DateTime? FullyPaidOnDate { get; set; }
		public bool? HasAttachments { get; set; }
		public string CurrencyCode { get; set; }
		public decimal? Total { get; set; }
		public decimal? TotalTax { get; set; }
		public decimal? SubTotal { get; set; }
		public string LineAmountTypes { get; set; }
		public string Status { get; set; }
		public Guid BrandingThemeId { get; set; }
		public DateTime? Date { get; set; }
		public Guid? ContactId { get; set; }
		public decimal? CurrencyRate { get; set; }
		public decimal? RemainingCredit { get; set; }
		public decimal? AppliedAmount { get; set; }
		public bool? SentToContact { get; set; }
		public string Reference { get; set; }
		public string Type { get; set; }
		public string Number { get; set; }
		public DateTime? DueDate { get; set; }
		public decimal? CisDeduction { get; set; }
		public DateTime? UpdatedDateUtc { get; set; }
	}
}
