#region NameSpace
using PaySpace.Api.PaySlips.Repository;
using SOARIntegration.PaySpace.Api.PaySlips.Repository;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace SOARIntegration.PaySpace.Api.PaySlips.Service
{
	public class PaySlipService : IPaySlipService
	{
		#region Private Fields
		private IPaySlipLeaveRepository<PaySlipLeave> _paySlipLeaveRepository;
		//private IRepository<PaySlipComponent> _paySlipComponentRepository;
		private IPaySlipComponentRepository<PaySlipComponent> _paySlipComponentRepository;
		#endregion

		#region CTOR
		public PaySlipService(IPaySlipLeaveRepository<PaySlipLeave> paySlipLeaveRepository,
							IPaySlipComponentRepository<PaySlipComponent> paySlipComponentRepository)
		{
			_paySlipLeaveRepository = paySlipLeaveRepository;
			_paySlipComponentRepository = paySlipComponentRepository;
		}
		#endregion

		#region InsertPaySlipLeaves
		public void InsertPaySlipLeaves(List<PaySlipLeave> paySlipLeaves)
		{
			try
			{
				var deletePaySlipLeaves = _paySlipLeaveRepository.GetRecordsOfCompanyByPaySlipDate(paySlipLeaves[0].PaySlipDate, paySlipLeaves[0].CompanyCode).ToList();

				if (deletePaySlipLeaves.Count > 0)
				{
					//_paySlipLeaveRepository.BulkDelete(deletePaySlipLeaves);					
				}

				//_paySlipLeaveRepository.BulkInsert(paySlipLeaves);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region InsertPaySlipComponents
		public void InsertPaySlipComponents(List<PaySlipComponent> paySlipComponents)
		{
			try
			{
				var deletePaySlipComponent = _paySlipComponentRepository.GetRecordsOfCompanyByPaySlipDate(paySlipComponents[0].PaySlipDate, paySlipComponents[0].CompanyCode).ToList();

				if (deletePaySlipComponent.Count > 0)
				{
					//_paySlipComponentRepository.BulkDelete(deletePaySlipComponent);					
				}

				//_paySlipComponentRepository.BulkInsert(paySlipComponents);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}
