using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Currencies.Data {
	public class CurrenciesMap
    {
        public CurrenciesMap(EntityTypeBuilder<Currency> entityBuilder) {
			entityBuilder.HasKey (t => t.Id);
            entityBuilder.Property (t => t.Code).IsRequired ();
		}
	}
}