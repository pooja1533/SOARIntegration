using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_Term")]
    public class Term : BaseEntity
    {
        public int Day { get; set; }

        public string TermType { get; set; }
    }
}