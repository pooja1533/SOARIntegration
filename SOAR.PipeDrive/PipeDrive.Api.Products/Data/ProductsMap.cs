#region NameSpace
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Products.Data
{
    public class ProductsMap
    {
		#region CTOR
		public ProductsMap(EntityTypeBuilder<Product> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Name).IsRequired();
		} 
		#endregion
	}
}