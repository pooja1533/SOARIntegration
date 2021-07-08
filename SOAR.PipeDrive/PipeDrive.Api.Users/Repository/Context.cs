using Microsoft.EntityFrameworkCore;
using SOARIntegration.PipeDrive.Api.Users.Data;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Users.Repository
{
    public class Context : DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new UsersMap(modelBuilder.Entity<User>());
		}
	}
}
