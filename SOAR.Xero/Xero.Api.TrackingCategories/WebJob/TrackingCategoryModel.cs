using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SOAR.Shared.Extensions;
using SOARIntegration.Xero.Api.TrackingCategories.Repository;
using SOARIntegration.Xero.Api.TrackingCategories.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.Core;
using Xero.Api.Core.Model;

namespace SOARIntegration.Xero.Api.TrackingCategories.WebJob
{
	public class TrackingCategoryModel
	{
		#region Private Fields
		private readonly ILogger<TrackingCategoryModel> logger;
		private readonly ITrackingService service;
		private readonly IXeroCoreApi api;
		private readonly Context context;
		private readonly IConfigurationRoot config;
		#endregion

		#region CTOR
		public TrackingCategoryModel(ILogger<TrackingCategoryModel> logger, ITrackingService svc, Context ctx, String orgKey, IConfigurationRoot cfg)
		{
			this.logger = logger;
			this.service = svc;
			this.context = ctx;
			this.config = cfg;
			api = SOARIntegration.Xero.Common.Helpers.Application.Initialise(orgKey);
		}
		#endregion

		public void Import()
		{
			Task.Run(() => this.ProcessDataAsync()).Wait();
		}

		public async System.Threading.Tasks.Task ProcessDataAsync()
		{
			try
			{
				logger.LogInformation("Running TrackingCategory web job on {0}", DateTime.Now.ToString());

				string org = config.GetValue<string>("XeroApi:Org");
				List<TrackingCategory> trackingCategoryAsList = new List<TrackingCategory>();

				var trackingCategoriesTemp = await api.TrackingCategories.IncludeArchived(true).FindAsync();
				trackingCategoryAsList = trackingCategoryAsList.Concat(trackingCategoriesTemp).ToList();
				await Task.Delay(2000);

				List<SOARIntegration.Xero.Common.Model.TrackingCategory> trackingCategoryList = new List<SOARIntegration.Xero.Common.Model.TrackingCategory>();
				List<SOARIntegration.Xero.Common.Model.TrackingOption> trackingOptionList = new List<SOARIntegration.Xero.Common.Model.TrackingOption>();
				for (var count = 0; count < trackingCategoryAsList.Count(); count++)
				{
					var trackingCategory = trackingCategoryAsList[count];
					trackingCategoryList.Add(MapResponseData(trackingCategory, org));
					trackingOptionList.AddRange(MapTrackingOptionResponseData(trackingCategory, org));
					logger.LogInformation("Tracking Category Name {0}", trackingCategory.Name);
				}

				service.InsertTrackingCategories(trackingCategoryList);
				service.InsertTrackingOptions(trackingOptionList);
				logger.LogInformation("Total tracking categories are {0}", trackingCategoryList.Count());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.PrintDetails());
			}

		}

		private SOARIntegration.Xero.Common.Model.TrackingCategory MapResponseData(TrackingCategory trackingCategory, string orgName)
		{
			SOARIntegration.Xero.Common.Model.TrackingCategory _trackingCategory = new SOARIntegration.Xero.Common.Model.TrackingCategory
			{
				Id = trackingCategory.Id,
				OrgName = orgName,
				Name = trackingCategory.Name,
				Status = trackingCategory.Status.ToString(),
				Audit_Created = DateTime.UtcNow
			};

			return _trackingCategory;
		}

		private List<SOARIntegration.Xero.Common.Model.TrackingOption> MapTrackingOptionResponseData(TrackingCategory trackingCategory, string orgName)
		{
			List<SOARIntegration.Xero.Common.Model.TrackingOption> trackingOptions = new List<SOARIntegration.Xero.Common.Model.TrackingOption>();
			for (var count = 0; count < trackingCategory.Options.Count; count++)
			{
				SOARIntegration.Xero.Common.Model.TrackingOption _trackingOption = new SOARIntegration.Xero.Common.Model.TrackingOption
				{
					Id = trackingCategory.Options[count].Id,
					OrgName = orgName,
					Name = trackingCategory.Options[count].Name,
					Status = trackingCategory.Options[count].Status.ToString(),
					TrackingCategoryId = trackingCategory.Id,
					Audit_Created = DateTime.UtcNow,
				};

				trackingOptions.Add(_trackingOption);
			}

			return trackingOptions;
		}

	}
}
