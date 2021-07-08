using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System.Collections.Generic;

namespace SOARIntegration.PaySpace.Api.Employees.Repository
{
	public interface IRepository<T> where T : Employee
	{
        T Get(string entityId);
        void Insert(T entity);
        void Update(T entity);
		int GetLastBatchId();
    }
}
