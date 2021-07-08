using SOARIntegration.Xero.Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_Payment")]
    public class Payment
    {
        public Guid Id { get; set; }
        public string OrgName { get; set; }
		public string Type { get; set; }
		public string Status { get; set; }
		public DateTime? Date { get; set; }
		public decimal? CurrencyRate { get; set; }
		public decimal? BankAmount { get; set; }
		public decimal? Amount { get; set; }
		public string Reference { get; set; }
		public bool? IsReconciled { get; set; }
		public Guid? InvoiceId { get; set; }
		public Guid? CreditNoteId { get; set; }
		public Guid? PrepaymentId { get; set; }
		public Guid? OverpaymentId { get; set; }
		public Guid? AccountId { get; set; }
    }
}
