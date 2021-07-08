using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_Currency")]
    public class Currency : BaseEntity
    {
        [Column("Code")]
        public string Code { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        public string OrgName { get; set; }
    }
}
