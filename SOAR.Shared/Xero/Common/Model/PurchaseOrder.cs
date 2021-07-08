using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_PurchaseOrder")]
    public class PurchaseOrder : BaseEntity
    {
        public Guid PurchaseOrderID { get; set; }

        public string OrgName { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public DateTime? PurchaseOrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? ExpectedArrivalDate { get; set; }

        public string DeliveryAddress { get; set; }

        public string AttentionTo { get; set; }

        public string Telephone { get; set; }

        public string DeliveryInstructions { get; set; }

        public decimal? TotalDiscount { get; set; }

        public bool SentToContact { get; set; }

        public string Reference { get; set; }

        public string CurrencyCode { get; set; }

        public decimal? CurrencyRate { get; set; }

        [InverseProperty("PurchaseOrder")]
        public PurchaseOrderContact Contact { get; set; }

        public string BrandingThemeID { get; set; }

        public string Status { get; set; }

        [InverseProperty("PurchaseOrder")]
        public List<PurchaseOrderLineItem> LineItems { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? TotalTax { get; set; }

        public decimal? Total { get; set; }

        public bool HasAttachments { get; set; }

        public DateTime? UpdatedDateUtc { get; set; }

    }
}
