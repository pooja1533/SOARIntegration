using System.Collections.Generic;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.PurchaseOrders.Service
{
	public interface IPurchaseOrdersService
    {
        void InsertPurchaseOrders(List<PurchaseOrder> purchaseOrders);

        IEnumerable<PurchaseOrder> GetAllPurchaseOrders();
	}
}
