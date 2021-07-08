#region NameSpace
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Stages.Data
{
    public class StagesMap
    {
		#region CTOR
		public StagesMap(EntityTypeBuilder<Stage> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
		} 
		#endregion
	}
}