using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_ExternalLink")]
    public class ExternalLink : BaseEntity
    {
        public string LinkType { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }
    }
}