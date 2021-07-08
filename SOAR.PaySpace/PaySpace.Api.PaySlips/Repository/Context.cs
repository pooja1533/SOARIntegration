#region NameSpace
using Microsoft.EntityFrameworkCore;
using SOARIntegration.PaySpace.Api.PaySlips.Data;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model; 
#endregion

namespace SOARIntegration.PaySpace.Api.PaySlips.Repository
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
			new PaySlipLeavesMap(modelBuilder.Entity<PaySlipLeave>());
			new PaySlipComponentsMap(modelBuilder.Entity<PaySlipComponent>());
		}
		#endregion

		public DbSet<PaySlipComponent> PaySlipComponents { get; set; }
		public DbSet<PaySlipLeave> PaySlipLeaves { get; set; }
	}
}
