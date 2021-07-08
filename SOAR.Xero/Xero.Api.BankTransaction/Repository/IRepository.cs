using SOARIntegration.SOAR.Shared.Xero.Common.Model;
using System;
using System.Collections.Generic;

namespace SOARIntegration.Xero.Api.BankTransactions.Repository
{
	public interface IRepository<T> where T : BankTransaction {
		IEnumerable<T> GetAll();
		T Get(String accountId);
		void Insert(T entity);
		void Update(T entity);
		void Delete(T entity);
		void Remove(T entity);
		void SaveChanges();
	}
}