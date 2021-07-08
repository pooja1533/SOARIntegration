using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Common.Model;
using SOARIntegration.Xero.Api.ExpenseClaims.Data;

namespace SOARIntegration.Xero.Api.ExpenseClaims.Repository
{
	public class Context:DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new ExpenseClaimsMap(modelBuilder.Entity<ExpenseClaim>());
		}
	}
}
