#region NameSpace
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model; 
#endregion

namespace SOARIntegration.PaySpace.Api.PaySlips.Data
{
	public class PaySlipLeavesMap
	{
		#region CTOR
		public PaySlipLeavesMap(EntityTypeBuilder<PaySlipLeave> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.EntityId).IsRequired();
		} 
		#endregion
	}
}
