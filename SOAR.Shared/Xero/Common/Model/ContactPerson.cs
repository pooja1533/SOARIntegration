using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_ContactPerson")]
    public class ContactPerson : BaseEntity
    {
        public string ContactPersonId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }
        
        public bool? IncludeInEmails { get; set; }
    }
}