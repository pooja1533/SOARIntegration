#region NameSpace
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SOARIntegration.PipeDrive.Api.Products.Repository;
using SOARIntegration.PipeDrive.Api.Products.Service;
using System;
using System.IO;
using System.Net; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Products.WebJob
{
	class Program
    {
        static void Main(string[] args)
        {
			var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");			
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<Context>(options => options.UseSqlServer(connectionString))
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddSingleton<IProductsService, ProductsService>()
                .BuildServiceProvider();

			var productService = serviceProvider.GetService<IProductsService>();
			ProductsData objProductData = new ProductsData(productService);
            objProductData.ProcessData();
        }
    }

    public partial class ProductsData
    {
		#region Private Fields
		private readonly IProductsService _productsService;
		#endregion

		#region CTOR
		public ProductsData(IProductsService productsService)
		{
			this._productsService = productsService;
		}
		#endregion

		#region ProcessData
		public void ProcessData()
		{
			try
			{
				var dataFound = true;
				// Note : pipedrive supports pagingwise data so I have created loop to fetch the data till the time data is coming.
				int start = 0;
				int end = 100;

				while (dataFound)
				{
					var pipeDriveToken = Environment.GetEnvironmentVariable("PipeDriveToken");
					string apiUrl = "https://a1l.pipedrive.com/v1/products?start=" + start + "&limit=" + end + "&api_token=" + pipeDriveToken;


					Uri address = new Uri(apiUrl);

					// Create the web request 
					HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

					request.Method = "GET";
					request.ContentType = "text/xml";

					using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
					{
						// Get the response stream 
						StreamReader reader = new StreamReader(response.GetResponseStream());
						string json = reader.ReadToEnd();

						// Note : Increment paging by 100
						start += 100;

						// Convert json to c# object
						var productObject = JSONToObject.FromJson(json);
						if (productObject == null || productObject.Data == null)
						{
							dataFound = false;
						}
						else
						{
							ProductModel objProductModel = new ProductModel(productObject);
							objProductModel.ProcessData(this._productsService);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + "\\n" + ex.StackTrace);
			}
		} 
		#endregion
	}
}
