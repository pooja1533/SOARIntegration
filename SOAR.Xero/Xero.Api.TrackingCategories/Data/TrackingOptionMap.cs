using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.TrackingCategories.Data
{
	public class TrackingOptionMap
	{
		#region CTOR
		public TrackingOptionMap(EntityTypeBuilder<TrackingOption> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).IsRequired();
		} 
		#endregion
	}
}
