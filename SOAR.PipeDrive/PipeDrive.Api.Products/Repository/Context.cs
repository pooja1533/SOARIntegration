#region NameSpace
using Microsoft.EntityFrameworkCore;
using SOARIntegration.PipeDrive.Api.Products.Data;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Products.Repository
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
			new ProductsMap(modelBuilder.Entity<Product>());
		} 
		#endregion
	}
}
