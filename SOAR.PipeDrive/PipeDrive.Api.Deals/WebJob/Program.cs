#region NameSpace
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SOARIntegration.PipeDrive.Api.Deals.Repository;
using SOARIntegration.PipeDrive.Api.Deals.Service;
using System;
using System.IO;
using System.Linq;
using System.Net;
#endregion

namespace SOARIntegration.PipeDrive.Api.Deals.WebJob
{
	public class Program
	{
		static void Main()
		{
			var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
			var serviceProvider = new ServiceCollection()
				.AddLogging()
				.AddDbContext<Context>(options => options.UseSqlServer(connectionString))
				.AddScoped(typeof(IRepository<>), typeof(Repository<>))
				.AddSingleton<IDealsService, DealService>()
				.BuildServiceProvider();

			var dealService = serviceProvider.GetService<IDealsService>();
			DealsData objUserData = new DealsData(dealService);
			objUserData.ProcessData();
		}
	}

	public partial class DealsData
	{
		#region Private Fields
		private readonly IDealsService _dealService;
		#endregion

		#region CTOR
		public DealsData(IDealsService dealsService)
		{
			this._dealService = dealsService;
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
					string apiUrl = "https://a1l.pipedrive.com/v1/deals?start=" + start + "&limit=" + end + "&api_token=" + pipeDriveToken;

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
						var dealObject = JSONToObject.FromJson(json);

						Console.WriteLine("Total Count :" + dealObject.Data.ToList().Count());

						if (dealObject == null || dealObject.Data == null)
						{
							dataFound = false;
						}
						else
						{
							DealModel objDealModel = new DealModel(dealObject);
							objDealModel.ProcessData(this._dealService);
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
