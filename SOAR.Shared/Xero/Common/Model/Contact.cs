using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_Contact")]
    public class Contact : BaseEntity
    {
        public string ContactID { get; set; }

        public string OrgName { get; set; }

        public string ContactStatus { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string SkypeUserName { get; set; }

        public string BankAccountDetails { get; set; }

        public string TaxNumber { get; set; }

        public string AccountsReceivableTaxType { get; set; }

        public string AccountsPayableTaxType { get; set; }

        public bool IsSupplier { get; set; }

        public bool IsCustomer { get; set; }

        public string DefaultCurrency { get; set; }

        public string Website { get; set; }

        public decimal? Discount { get; set; }

        public string XeroNetworkKey { get; set; }

        public bool HasAttachments { get; set; }

        public string PurchaseAccountCode { get; set; }

        public string SalesAccountCode { get; set; }

        /*public PaymentTerms PaymentTerms { get; set; }

        public List<ContactPerson> ContactPersons { get; set; }

        public List<Address> Addresses { get; set; }

        public List<Phone> Phones { get; set; }

        public List<ContactGroup> ContactGroups { get; set; }*/

        public string AccountNumber { get; set; } 
    }
}