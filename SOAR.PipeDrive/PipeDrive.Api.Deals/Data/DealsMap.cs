#region NameSpace
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Deals.Data
{
	public class DealsMap
	{
		#region CTOR
		public DealsMap(EntityTypeBuilder<Deal> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.EntityId).IsRequired();
		} 
		#endregion
	}
}
