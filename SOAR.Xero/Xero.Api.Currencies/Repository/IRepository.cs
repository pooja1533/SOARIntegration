using System.Collections.Generic;
using SOARIntegration.Xero.Common.Model;
using System;
namespace SOARIntegration.Xero.Api.Currencies.Repository {
    public interface IRepository<T> where T : Currency {
		IEnumerable<T> GetAll ();
		T Get (String code);
		void Insert (T entity);
		void Update (T entity);
		void Delete (T entity);
		void RemoveRange (T[] entity);
		void SaveChanges ();
	}
}