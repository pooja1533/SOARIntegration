#region NameSpace
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model; 
#endregion

namespace SOARIntegration.PaySpace.Api.PaySlips.Data
{
	public class PaySlipComponentsMap
	{
		#region CTOR
		public PaySlipComponentsMap(EntityTypeBuilder<PaySlipComponent> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.EntityId).IsRequired();
		} 
		#endregion
	}
}
