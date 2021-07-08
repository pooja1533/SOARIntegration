using System;
using System.Collections.Generic;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.PurchaseOrders.Repository {
	public interface IRepository<T> where T : BaseEntity {
		IEnumerable<T> GetAll ();
		T Get (Guid purchaseOrderId);
		void Insert (T entity);
		void Update (T entity);
		void Delete (T entity);
		void Remove (T entity);
		void SaveChanges ();
	}
}