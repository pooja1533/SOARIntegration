using System.Runtime.Serialization;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_Account")]
    public class Account : BaseEntity
    {
       
        public string AccountId { get; set; }

        public string OrgName { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public string BankAccountType { get; set; }

        public string TaxType { get; set; }

        public string Description { get; set; }

        public string Class { get; set; }

        public string SystemAccount { get; set; }

        public bool? EnablePaymentsToAccount { get; set; }

        public bool? ShowInExpenseClaims { get; set; }

        public string BankAccountNumber { get; set; }

        public string CurrencyCode { get; set; }

        public string ReportingCode { get; set; }

        public string ReportingCodeName { get; set; }

        public bool? HasAttachments { get; set; }
    }
}