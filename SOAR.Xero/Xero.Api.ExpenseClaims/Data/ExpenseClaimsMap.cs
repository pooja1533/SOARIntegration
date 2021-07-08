using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.ExpenseClaims.Data {
	public class ExpenseClaimsMap
    {
		public ExpenseClaimsMap(EntityTypeBuilder<ExpenseClaim> entityBuilder) {
			entityBuilder.HasKey (t => t.Id);
            entityBuilder.Property (t => t.Id).IsRequired ();
		}
	}
}