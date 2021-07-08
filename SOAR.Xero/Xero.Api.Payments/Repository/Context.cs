using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Common.Model;
using SOARIntegration.Xero.Api.Payments.Data;
using SOAR.Shared.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Payments.Repository
{
	public class Context:DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new PaymentsMap(modelBuilder.Entity<Payment>());
		}
	}
}
