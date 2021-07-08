using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.TrackingCategories.Data
{
	public class TrackingCategoryMap
	{
		#region CTOR
		public TrackingCategoryMap(EntityTypeBuilder<TrackingCategory> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).IsRequired();
		} 
		#endregion
	}
}
