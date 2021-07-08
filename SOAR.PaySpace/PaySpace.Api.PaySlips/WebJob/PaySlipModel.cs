#region NameSpaces
using SOARIntegration.PaySpace.Api.PaySlips.Service;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System;
using System.Collections.Generic;

#endregion
namespace SOARIntegration.PaySpace.Api.PaySlips.WebJob
{
	public class PaySlipModel
	{
		#region Private Fields
		private List<PaySlipComponent> _paySlipComponents;
		private List<PaySlipLeave> _paySlipLeaves;
		#endregion

		#region CTOR
		public PaySlipModel(List<PaySlipComponent> paySlipComponents, List<PaySlipLeave> paySlipLeaves)
		{
			_paySlipComponents = paySlipComponents;
			_paySlipLeaves = paySlipLeaves;
		}
		#endregion

		#region ProcessData
		public void ProcessData(IPaySlipService paySlipService)
		{
			try
			{
				paySlipService.InsertPaySlipComponents(_paySlipComponents);
				paySlipService.InsertPaySlipLeaves(_paySlipLeaves);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}
