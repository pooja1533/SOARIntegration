using SOARIntegration.Xero.Common.Model;
using System.Collections.Generic;

namespace SOARIntegration.Xero.Api.TrackingCategories.Service
{
	public interface ITrackingService
	{
		void InsertTrackingCategories(List<TrackingCategory> trackingCategories);
		void InsertTrackingOptions(List<TrackingOption> trackingOptions);

		IEnumerable<TrackingCategory> GetTrackingCategories();
		IEnumerable<TrackingOption> GetTrackingOptions();
	}
}
