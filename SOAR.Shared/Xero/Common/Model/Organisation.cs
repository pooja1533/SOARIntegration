using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_Organisation")]
    public class Organisation : BaseEntity
    {
        public string OrganisationId { get; set; }

        public string Name { get; set; }

        public string LegalName { get; set; }

        public string ShortCode { get; set; }

        public bool? PaysTax { get; set; }

        public string Version { get; set; }

        public string OrganisationType { get; set; }

        public bool? IsDemoCompany { get; set; }

        public string BaseCurrency { get; set; }

        public string ApiKey { get; set; }

        public string CountryCode { get; set; }

        public string OrganisationStatus { get; set; }

        public string TaxNumber { get; set; }

        public int? FinancialYearEndDay { get; set; }

        public int? FinancialYearEndMonth { get; set; }

        public DateTime? PeriodLockDate { get; set; }
        
        public DateTime? EndOfYearLockDate { get; set; }

        public DateTime? CreatedDateUtc { get; set; }

        public string Timezone { get; set; }

        public string LineOfBusiness { get; set; }

        public string RegistrationNumber { get; set; }

        //public PaymentTerms PaymentTerms { get; set; }

        public string SalesTaxBasisType { get; set; }

        public string SalesTaxPeriod { get; set; }

       [InverseProperty("Organisation")]
        public List<OrganisationAddress> Addresses { get; set; }

        [InverseProperty("Organisation")]
        public List<OrganisationPhone> Phones { get; set; }

       // public List<ExternalLink> ExternalLinks { get; set; }
    }
}
