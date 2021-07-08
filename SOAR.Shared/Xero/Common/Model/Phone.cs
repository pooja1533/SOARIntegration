using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    public class Phone : BaseEntity
    {
        public string PhoneType { get; set; }

        public string PhoneNumber { get; set; }
        
        public string PhoneAreaCode { get; set; }
        
        public string PhoneCountryCode { get; set; }        
    }
}
