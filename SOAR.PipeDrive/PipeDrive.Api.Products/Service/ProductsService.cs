#region NameSpace
using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Products.Repository;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Products.Service
{
    public class ProductsService : IProductsService
    {
		#region Private Fields
		private IRepository<Product> _repository;
		#endregion

		#region CTOR
		public ProductsService(IRepository<Product> repository)
		{
			this._repository = repository;
		}
		#endregion

		#region InsertProducts
		public void InsertProducts(List<Product> products)
		{
			for (var count = 0; count < products.Count; count++)
			{
				try
				{
					var product = _repository.Get(products[count].EntityId);
					if (product == null)
					{
						_repository.Insert(products[count]);
					}
					else
					{
						products[count].Id = product.Id;
						products[count].Audit_Created = product.Audit_Created;
						products[count].Audit_Modified = product.Audit_Modified;
						products[count].Audit_Deleted = product.Audit_Deleted;
						_repository.Update(products[count]);
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		} 
		#endregion
	}
}
