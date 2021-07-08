using System.Collections.Generic;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Deals.Service
{
    public interface IDealsService
	{
		void InsertDeals(List<Deal> deals);
	}
}
