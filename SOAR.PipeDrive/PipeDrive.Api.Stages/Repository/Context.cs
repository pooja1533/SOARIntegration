#region ProcessData
using Microsoft.EntityFrameworkCore;
using SOARIntegration.PipeDrive.Api.Stages.Data;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Stages.Repository
{
    public class Context:DbContext
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
			new StagesMap(modelBuilder.Entity<Stage>());
		} 
		#endregion
	}
}
