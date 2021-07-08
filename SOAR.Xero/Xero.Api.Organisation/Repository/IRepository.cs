using System.Collections.Generic;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Organisations.Repository {
	public interface IRepository<T> where T : BaseEntity {
		IEnumerable<T> GetAll ();
		T Get (string organisationId);
		void Insert (T entity);
		void Update (T entity);
		void Delete (T entity);
		void Remove (T entity);
		void SaveChanges ();
	}
}