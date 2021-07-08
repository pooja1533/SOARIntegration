using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_Xero_Invoice")]
    public class Invoice
    {
        public Guid Id { get; set; }
        public string OrgName { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string LineAmountTypes { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ExpectedPaymentDate { get; set; }
        public DateTime? PlannedPaymentDate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalDiscount { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? CurrencyRate { get; set; }
        public DateTime? FullyPaidOnDate { get; set; }
        public decimal? AmountDue { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? AmountCredited { get; set; }
        public bool? HasAttachments { get; set; }
        public Guid? BrandingThemeId { get; set; }
        public string Url { get; set; }
        public string Reference { get; set; }
        public bool? SentToContact { get; set; }
        public decimal? CisDeduction { get; set; }
		public Guid? ContactId { get; set; }
    }
}
