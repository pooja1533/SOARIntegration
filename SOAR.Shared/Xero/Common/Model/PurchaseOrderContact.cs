using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_PurchaseOrderContact")]
    public class PurchaseOrderContact : Contact
    {
        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder PurchaseOrder { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public int PurchaseOrderId { get; set; }
    }
}