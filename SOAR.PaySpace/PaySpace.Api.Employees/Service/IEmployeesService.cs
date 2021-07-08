using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System.Collections.Generic;

namespace SOARIntegration.PaySpace.Api.Employees.Service
{
	public interface IEmployeesService
	{
		void InsertEmployees(List<Employee> employees);
		int GetLastBatchId();
	}
}
