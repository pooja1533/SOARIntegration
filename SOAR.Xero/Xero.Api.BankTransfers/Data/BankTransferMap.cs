using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.BankTransfers.Data
{
	public class BankTransferMap
	{
		#region CTOR
		public BankTransferMap(EntityTypeBuilder<BankTransfer> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Id).IsRequired();
		} 
		#endregion
	}
}
