#region CTOR
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SOARIntegration.PipeDrive.Api.Stages.Repository;
using SOARIntegration.PipeDrive.Api.Stages.Service;
using System;
using System.IO;
using System.Net;
#endregion

namespace SOARIntegration.PipeDrive.Api.Stages.WebJob
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
				.AddSingleton<IStagesService, StagesService>()
				.BuildServiceProvider();

			var stageservice = serviceProvider.GetService<IStagesService>();

			StagesData objStageData = new StagesData(stageservice);
			objStageData.ProcessData();
		}
	}

	public partial class StagesData
	{
		#region Private Fields
		private readonly IStagesService _stagesService;
		#endregion

		#region CTOR
		public StagesData(IStagesService stagesService)
		{
			this._stagesService = stagesService;
		}
		#endregion

		#region ProcessData
		public void ProcessData()
		{
			try
			{
				var pipeDriveToken = Environment.GetEnvironmentVariable("PipeDriveToken");
				string apiUrl = "https://a1l.pipedrive.com/v1/stages?api_token=" + pipeDriveToken;

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
					var stageObject = JSONToObject.FromJson(json);
					StageModel objStageModel = new StageModel(stageObject);
					objStageModel.ProcessData(this._stagesService);
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
