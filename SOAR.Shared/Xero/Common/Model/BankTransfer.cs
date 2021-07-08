using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_BankTransfer")]
    public class BankTransfer
    {
        public Guid Id { get; set; }
		public string OrgName { get; set; }
        public DateTime? TransferDate { get; set; }
        public Guid FromBankTransactionId { get; set; }
		public Guid ToBankTransactionId { get; set; }
		//[Column(TypeName = "decimal(18,2)")]
		public decimal? Amount { get; set; }
		public Guid FromBankAccountId { get; set; }
		public string FromBankAccountName { get; set; }
		public string FromBankAccountCode { get; set; }
		public Guid ToBankAccountId { get; set; }
		public string ToBankAccountName { get; set; }
		public string ToBankAccountCode { get; set; }
		[Column(TypeName = "decimal(18,6)")]
		public decimal? CurrencyRate { get; set; }
		public bool? HasAttachments { get; set; }
		public DateTime Audit_Created { get; set; }
	}
}
