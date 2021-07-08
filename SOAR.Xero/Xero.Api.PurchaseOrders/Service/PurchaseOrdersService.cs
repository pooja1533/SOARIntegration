using SOARIntegration.Xero.Common.Model;
using SOARIntegration.Xero.Api.PurchaseOrders.Repository;
using System.Collections.Generic;
using System;

namespace SOARIntegration.Xero.Api.PurchaseOrders.Service
{
	public class PurchaseOrdersService : IPurchaseOrdersService
    {
        private readonly IRepository<PurchaseOrder> _repository;

        public PurchaseOrdersService(IRepository<PurchaseOrder> repository)
		{
			this._repository = repository;
		}

        public void InsertPurchaseOrders(List<PurchaseOrder> purchaseOrders)
        {
            int saveCounter = 0;
            for (var count = 0; count < purchaseOrders.Count; count++)
            {
                try
                {
                    var po = _repository.Get(purchaseOrders[count].PurchaseOrderID);
                    if (po == null)
                    {
                        _repository.Insert(purchaseOrders[count]);
                    }
                    else
                    {
                        purchaseOrders[count].Id = po.Id;
                        purchaseOrders[count].Created = po.Created;
                        purchaseOrders[count].Contact.Id = po.Contact.Id;
                        _repository.Update(purchaseOrders[count]);
                    }
                    saveCounter++;

                    if (saveCounter % 500 == 0)
                    {
                        _repository.SaveChanges();
                        Console.Write(".");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            _repository.SaveChanges();
            Console.WriteLine($"Completed updating {purchaseOrders.Count} purchase orders.");
        }

        public IEnumerable<PurchaseOrder> GetAllPurchaseOrders()
		{
			return _repository.GetAll();
		}
	}
}
