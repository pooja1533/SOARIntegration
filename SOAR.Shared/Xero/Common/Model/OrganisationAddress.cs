using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_OrganisationAddress")]
    public class OrganisationAddress : Address
    {
       
        [ForeignKey("OrganisationId")]
        public Organisation Organisation { get; set; }

        [ForeignKey("OrganisationId")]
        public int OrganisationId { get; set; }
    }
}
