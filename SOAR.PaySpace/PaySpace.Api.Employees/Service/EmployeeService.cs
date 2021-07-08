#region NameSpace
using SOARIntegration.PaySpace.Api.Employees.Repository;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System;
using System.Collections.Generic;
#endregion

namespace SOARIntegration.PaySpace.Api.Employees.Service
{
	public class EmployeeService : IEmployeesService
	{
		#region Private Fields
		private IRepository<Employee> _employeeRepository;
		#endregion

		#region CTOR
		public EmployeeService(IRepository<Employee> employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}
		#endregion

		#region InsertEmployees
		public void InsertEmployees(List<Employee> employees)
		{
			for (var count = 0; count < employees.Count; count++)
			{
				try
				{
					var employee = _employeeRepository.Get(employees[count].EntityId);
					if (employee == null)
					{
						_employeeRepository.Insert(employees[count]);
					}
					else
					{
						employees[count].Id = employee.Id;
						employees[count].Audit_Created = employee.Audit_Created;
						employees[count].Audit_Modified = DateTime.UtcNow;
						employees[count].Audit_Deleted = employee.Audit_Deleted;
						_employeeRepository.Update(employees[count]);
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		#endregion

		#region GetLastBatchId
		public int GetLastBatchId()
		{
			try
			{
				return _employeeRepository.GetLastBatchId();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		} 
		#endregion
	}
}