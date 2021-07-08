using SOAR.Shared.Xero.Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_PurchaseOrderLineItem")]
    public class PurchaseOrderLineItem : LineItem
    {

        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder PurchaseOrder { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public int PurchaseOrderId { get; set; }

		[Column("Audit_Created")]
		public DateTime Created { get; set; }
		[Column("Audit_Modified")]
		public DateTime Modified { get; set; }

	}
}
