using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOAR.Shared.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Payments.Data {
	public class PaymentsMap
    {
		public PaymentsMap(EntityTypeBuilder<Payment> entityBuilder) {
			entityBuilder.HasKey (t => t.Id);
			entityBuilder.Property (t => t.Id).IsRequired ();
		}
	}
}