#region NameSpace
using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Deals.Repository;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Deals.Service
{
    public class DealService : IDealsService
	{
		#region Private Fields
		private IRepository<Deal> _dealRepository;
		#endregion

		#region CTOR
		public DealService(IRepository<Deal> dealRepository)
		{
			_dealRepository = dealRepository;
		}
		#endregion

		#region InsertDeals
		public void InsertDeals(List<Deal> deals)
		{
			for (var count = 0; count < deals.Count; count++)
			{
				try
				{
					var deal = _dealRepository.Get(deals[count].EntityId);
					if (deal == null)
					{
						_dealRepository.Insert(deals[count]);
					}
					else
					{
						deals[count].Id = deal.Id;
						deals[count].Audit_Created = deal.Audit_Created;
						deals[count].Audit_Modified = deal.Audit_Modified;
						deals[count].Audit_Deleted = deal.Audit_Deleted;
						_dealRepository.Update(deals[count]);
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		} 
		#endregion
	}
}
