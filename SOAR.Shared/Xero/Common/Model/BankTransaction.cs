using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_Xero_BankTransaction")]
	public class BankTransaction
	{
		public Guid Id { get; set; }
		public string OrgName { get; set; }
		public Guid? ContactId { get; set; }
		public string ContactName { get; set; }
		public DateTime? Date { get; set; }
		public string Status { get; set; }
		public string LineAmountTypes { get; set; }
		[Column(TypeName ="decimal(18,2)")]
		public decimal? SubTotal { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal? TotalTax { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal? Total { get; set; }
		public string CurrencyCode { get; set; }
		public Guid? AccountId { get; set; }
		public string AccountName { get; set; }
		public string AccountCode { get; set; }
		public string Type { get; set; }
		public string Reference { get; set; }
		public string IsReconciled { get; set; }
		public DateTime Audit_Created { get; set; }
	}
}
