#region NameSpace
using Microsoft.EntityFrameworkCore;
using SOARIntegration.PaySpace.Api.Employees.Data;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
#endregion

namespace SOARIntegration.PaySpace.Api.Employees.Repository
{
	public class Context : DbContext
	{
		#region CTOR
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		#endregion

		#region OnModelCreating
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new EmployeesMap(modelBuilder.Entity<Employee>());
		} 
		#endregion
	}
}
