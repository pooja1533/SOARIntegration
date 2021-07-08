#region NameSpace
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model; 
#endregion

namespace SOARIntegration.PaySpace.Api.Employees.Data
{
	public class EmployeesMap
	{
		#region CTOR
		public EmployeesMap(EntityTypeBuilder<Employee> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.EntityId).IsRequired();
		} 
		#endregion
	}
}
