using SOARIntegration.Xero.Api.TrackingCategories.Repository;
using SOARIntegration.Xero.Api.TrackingCategories.Service;
using SOARIntegration.Xero.Common.Model;
using System;
using System.Collections.Generic;

namespace XeroSOARIntegration.Xero.Api.TrackingCategories.Service
{
	public class TrackingService : ITrackingService
	{
		private ITrackingCategoryRepository<TrackingCategory> _repository;
		private ITrackingOptionRepository<TrackingOption> _trackingOptionRepository;

		public TrackingService(ITrackingCategoryRepository<TrackingCategory> repository, ITrackingOptionRepository<TrackingOption> trackingOptionRepository)
		{
			this._repository = repository;
			this._trackingOptionRepository = trackingOptionRepository;
		}

		public void InsertTrackingCategories(List<TrackingCategory> trackingCategories)
		{
			int saveCounter = 0;
			for (var count = 0; count < trackingCategories.Count; count++)
			{
				try
				{
					var org = _repository.Get(trackingCategories[count].Id);
					if (org == null)
					{
						_repository.Insert(trackingCategories[count]);
					}
					else
					{
						trackingCategories[count].Id = org.Id;
						_repository.Update(trackingCategories[count]);
					}
					saveCounter++;
					if (saveCounter % 500 == 0)
					{
						_repository.SaveChanges();
						Console.Write(".");
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			_repository.SaveChanges();
			Console.WriteLine("Completed updating payments.");
		}

		public IEnumerable<TrackingCategory> GetTrackingCategories()
		{
			return _repository.GetAll();
		}

		public void InsertTrackingOptions(List<TrackingOption> trackingOptions)
		{
			int saveCounter = 0;
			for (var count = 0; count < trackingOptions.Count; count++)
			{
				try
				{
					var org = _trackingOptionRepository.Get(trackingOptions[count].Id);
					if (org == null)
					{
						_trackingOptionRepository.Insert(trackingOptions[count]);
					}
					else
					{
						trackingOptions[count].Id = org.Id;
						_trackingOptionRepository.Update(trackingOptions[count]);
					}
					saveCounter++;
					if (saveCounter % 500 == 0)
					{
						_repository.SaveChanges();
						Console.Write(".");
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			_repository.SaveChanges();
			Console.WriteLine("Completed updating payments.");
		}

		public IEnumerable<TrackingOption> GetTrackingOptions()
		{
			return _trackingOptionRepository.GetAll();
		}
	}
}
