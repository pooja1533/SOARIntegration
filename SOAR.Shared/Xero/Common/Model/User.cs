using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_User")]
    public class User : BaseEntity
    {
        public string UserID { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsSubscriber { get; set; }

        public string OrganisationRole { get; set; }
    }
}
