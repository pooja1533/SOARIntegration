using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Common.Model;
using Xero.Api.Contacts.Data;

namespace Xero.Api.Contacts.Repository
{
    public class Context:DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            new ContactMap(modelBuilder.Entity<Contact>());
        }
    }
}
