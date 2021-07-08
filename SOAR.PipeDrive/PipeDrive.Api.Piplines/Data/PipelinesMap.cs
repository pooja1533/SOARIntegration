#region NameSpace
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Pipelines.Data
{
    public class PipelinesMap
    {
		#region CTOR
		public PipelinesMap(EntityTypeBuilder<Pipeline> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Name).IsRequired();
		} 
		#endregion
	}
}