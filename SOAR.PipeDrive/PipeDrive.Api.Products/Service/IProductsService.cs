using System.Collections.Generic;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Products.Service
{
    public interface IProductsService
    {
        void InsertProducts(List<Product> products);
    }
}
