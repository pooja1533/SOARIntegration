using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_ItemTracking")]
    public class ItemTrackingCategory : BaseEntity
    {
        public string TrackingCategoryID { get; set; }

        public string Name { get; set; }

        public string Option { get; set; }

        public string OptionId { get; set; }

        [ForeignKey("PurchaseOrderLineItemId")]
        public PurchaseOrderLineItem LineItem { get; set; }

        [ForeignKey("PurchaseOrderLineItemId")]
        public int PurchaseOrderLineItemId { get; set; }
    }
}