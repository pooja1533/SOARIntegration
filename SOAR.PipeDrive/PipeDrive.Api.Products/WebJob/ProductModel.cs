#region NameSpace
using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Products.Service;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Products.WebJob
{
    public class ProductModel
    {
        #region Private Fields
        private JSONToObject _productList;
        #endregion

        #region CTOR
        public ProductModel(JSONToObject product)
        {
            _productList = product;
        }
        #endregion

        #region ProcessData
        public void ProcessData(IProductsService productsService)
        {
            try
            {
                List<Product> productList = new List<Product>();
                foreach (var product in _productList.Data)
                {
                    var objDeal = MapFields(product);
                    productList.Add(objDeal);
                }

                productsService.InsertProducts(productList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Map Fields
        private Product MapFields(ProductData product)
        {
            Product objProduct = new Product();
            objProduct.EntityId = product.Id.ToString();
            objProduct.Name = product.Name;
            objProduct.Code = product.Code;
            objProduct.Unit = product.Unit;
            objProduct.Tax = product.Tax;
            objProduct.Active_Flag = product.ActiveFlag;
            objProduct.Selectable = product.Selectable;
			objProduct.First_Char = product.FirstChar;
			objProduct.Visible_To = product.VisibleTo;
			objProduct.Owner_Id = product.OwnerId.Id;
			objProduct.Files_Count = product.FilesCount;
			objProduct.Followers_Count = product.FollowersCount;
			objProduct.Add_Time = product.AddTime;
			objProduct.Update_Time = product.UpdateTime;

            return objProduct;
        }

        #endregion
    }
}
