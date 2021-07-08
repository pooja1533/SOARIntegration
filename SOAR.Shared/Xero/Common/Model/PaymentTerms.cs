using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_PaymentTerms")]
    public class PaymentTerms : BaseEntity
    {
        public Term Bills { get; set; }

        public Term Sales { get; set; }
    }
}