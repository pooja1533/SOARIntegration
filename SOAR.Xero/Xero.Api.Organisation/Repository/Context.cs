using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Common.Model;
using SOARIntegration.Xero.Api.Organisations.Data;

namespace SOARIntegration.Xero.Api.Organisations.Repository
{
	public class Context:DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new OrganisationsMap(modelBuilder.Entity<Organisation>());
		}
	}
}
