using System.Collections.Generic;
using SOARIntegration.Xero.Common.Model;
using System;

namespace SOARIntegration.Xero.Api.ExpenseClaims.Repository {
    public interface IRepository<T> where T : ExpenseClaim {
		IEnumerable<T> GetAll ();
		T Get (String entityId);
		void Insert (T entity);
		void Update (T entity);
		void Delete (T entity);
		void Remove (T entity);
		void SaveChanges ();
	}
}