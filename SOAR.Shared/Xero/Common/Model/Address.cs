using Xero.Api.Core.Model.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    public class Address : BaseEntity
    {
        public string AddressType { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }
        
        public string AddressLine3 { get; set; }

        public string AddressLine4 { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string AttentionTo { get; set; }
    }
}
