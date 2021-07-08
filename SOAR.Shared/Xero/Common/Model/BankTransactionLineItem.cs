using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_Xero_BankTransactionLineItem")]
    public class BankTransactionLineItem
    {
		public Guid LineItemId { get; set; }
		public Guid ReferenceId { get; set; }
		public string Description { get; set; }
		public decimal? Quantity { get; set; }
		public decimal? UnitAmount { get; set; }
		public string AccountCode { get; set; }
		public string ItemCode { get; set; }
		public string TaxType { get; set; }
		public decimal? TaxAmount { get; set; }
		public decimal? LineAmount { get; set; }
		public decimal? DiscountRate { get; set; }
	}
}
