using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.PurchaseOrders.Data {
	public class PurchaseOrdersMap
    {
		public PurchaseOrdersMap(EntityTypeBuilder<PurchaseOrder> entityBuilder) {
			entityBuilder.HasKey (t => t.Id);
			entityBuilder.Property (t => t.PurchaseOrderID).IsRequired ();
		}
	}
}