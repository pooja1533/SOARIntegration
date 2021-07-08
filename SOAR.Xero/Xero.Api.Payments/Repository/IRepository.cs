using System;
using System.Collections.Generic;
using SOAR.Shared.Xero.Common.Model;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Payments.Repository {
	public interface IRepository<T> where T : Payment {
		IEnumerable<T> GetAll ();
		T Get (Guid paymentId);
		void Insert (T entity);
		void Update (T entity);
		void Delete (T entity);
		void Remove (T entity);
		void SaveChanges ();
	}
}