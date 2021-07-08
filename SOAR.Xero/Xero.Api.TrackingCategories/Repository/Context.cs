using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Api.TrackingCategories.Data;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.TrackingCategories.Repository
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
			new TrackingCategoryMap(modelBuilder.Entity<TrackingCategory>());
			new TrackingOptionMap(modelBuilder.Entity<TrackingOption>());
		} 
		#endregion
	}
}
