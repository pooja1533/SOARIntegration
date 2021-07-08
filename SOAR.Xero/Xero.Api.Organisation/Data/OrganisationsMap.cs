using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Organisations.Data {
	public class OrganisationsMap
    {
		public OrganisationsMap(EntityTypeBuilder<Organisation> entityBuilder) {
			entityBuilder.HasKey (t => t.Id);
			entityBuilder.Property (t => t.Name).IsRequired ();
		}
	}
}