#region NameSpace
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SOARIntegration.PipeDrive.Api.Pipelines.Repository;
using SOARIntegration.PipeDrive.Api.Pipelines.Service;
using System;
using System.IO;
using System.Net;
#endregion

namespace SOARIntegration.PipeDrive.Api.Pipelines.WebJob
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
				.AddSingleton<IPipelinesService, PipelinesService>()
				.BuildServiceProvider();

			var pipelineService = serviceProvider.GetService<IPipelinesService>();
			PipelinesData objPipelineData = new PipelinesData(pipelineService);
			objPipelineData.ProcessData();
		}
	}

	public partial class PipelinesData
	{
		#region Private Fields
		private readonly IPipelinesService _pipelinesService;
		#endregion

		#region CTOR
		public PipelinesData(IPipelinesService pipelinesService)
		{
			this._pipelinesService = pipelinesService;
		}
		#endregion

		#region ProcessData
		public void ProcessData()
		{
			try
			{
				var pipeDriveToken = Environment.GetEnvironmentVariable("PipeDriveToken");
				string apiUrl = "https://a1l.pipedrive.com/v1/pipelines?api_token=" + pipeDriveToken;

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

					// Convert json to c# object
					var pipelineObject = JSONToObject.FromJson(json);
					PipelineModel objPipelineModel = new PipelineModel(pipelineObject);
					objPipelineModel.ProcessData(this._pipelinesService);
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
