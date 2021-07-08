#region NameSpaces
using SOARIntegration.PaySpace.Api.Employees.Service;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System;
using System.Collections.Generic;
#endregion

namespace SOARIntegration.PaySpace.Api.Employees.WebJob
{
	public class EmployeeModel
	{
		#region Private Fields
		private List<Employee> _employees;
		#endregion

		#region CTOR
		public EmployeeModel() { }
		public EmployeeModel(List<Employee> employees)
		{
			_employees = employees;
		}
		#endregion

		#region ProcessData
		public void ProcessData(IEmployeesService employeeService)
		{
			try
			{
				employeeService.InsertEmployees(_employees);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region GetLastBatchId
		public int GetLastBatchId(IEmployeesService employeeService)
		{
			try
			{
				return employeeService.GetLastBatchId();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		} 
		#endregion
	}
}
