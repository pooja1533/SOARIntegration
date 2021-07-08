#region NameSpace
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SOARIntegration.PipeDrive.Api.Organizations.Repository;
using SOARIntegration.PipeDrive.Api.Organizations.Service;
using System;
using System.IO;
using System.Net;
#endregion

namespace SOARIntegration.PipeDrive.Api.Organizations.WebJob
{
	public class Program
	{
		static void Main(string[] args)
		{
			var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
			var serviceProvider = new ServiceCollection()
				.AddLogging()
				.AddDbContext<Context>(options => options.UseSqlServer(connectionString))
				.AddScoped(typeof(IRepository<>), typeof(Repository<>))
				.AddSingleton<IOrganizationsService, OrganizationsService>()
				.BuildServiceProvider();

			var organizationService = serviceProvider.GetService<IOrganizationsService>();

			OrganizationsData objOrganizationsData = new OrganizationsData(organizationService);
			objOrganizationsData.ProcessData();
		}
	}

	public partial class OrganizationsData
	{
		#region Private Fields
		private readonly IOrganizationsService _organizationsService;
		#endregion

		#region CTOR
		public OrganizationsData(IOrganizationsService organizationsService)
		{
			this._organizationsService = organizationsService;
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
					string apiUrl = "https://a1l.pipedrive.com/v1/organizations?start=" + start + "&limit=" + end + "&api_token=" + pipeDriveToken;


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
						var organizationObject = JSONToObject.FromJson(json);

						if (organizationObject == null || organizationObject.Data == null)
						{
							dataFound = false;
						}
						else
						{
							OrganizationModel objorganizationModel = new OrganizationModel(organizationObject);
							objorganizationModel.ProcessData(this._organizationsService);
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
