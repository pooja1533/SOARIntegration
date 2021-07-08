using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.SOAR.Shared.Xero.Common.Model;

namespace Xero.Api.BankTransactions.Data
{
	public class BankTransactionsMap
	{
		public BankTransactionsMap(EntityTypeBuilder<BankTransaction> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).IsRequired();
		}
	}
}
