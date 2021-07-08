#region NameSpace
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Organizations.Data
{
    public class OrganizationsMap
    {
		#region CTOR
		public OrganizationsMap(EntityTypeBuilder<Organization> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
		} 
		#endregion
	}
}