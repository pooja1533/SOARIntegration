#region NameSpace
using Microsoft.EntityFrameworkCore;
using SOARIntegration.PipeDrive.Api.Deals.Data;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Deals.Repository
{
    public class Context : DbContext
	{
		#region CTOR
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		#endregion

		#region MyRegion
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new DealsMap(modelBuilder.Entity<Deal>());
		} 
		#endregion
	}
}
