using SOARIntegration.Xero.Common.Model;
using System;
using System.Collections.Generic;

namespace SOARIntegration.Xero.Api.BankTransfers.Repository
{
	public interface IRepository<T> where T : BankTransfer
	{
		IEnumerable<T> GetAll();
		T Get(Guid Id);
		T Get(string organisationId);
		void Insert(T entity);
		void Update(T entity);
		void Delete(T entity);
		void Remove(T entity);
		void SaveChanges();
	}
}
