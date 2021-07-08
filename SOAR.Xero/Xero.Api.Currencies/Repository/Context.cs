using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Api.Currencies.Data;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Currencies.Repository
{
	public class Context : DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            new CurrenciesMap(modelBuilder.Entity<Currency>());
		}
	}
}
