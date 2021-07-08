using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System.Collections.Generic;

namespace SOARIntegration.PaySpace.Api.PaySlips.Service
{
	public interface IPaySlipService
	{
		void InsertPaySlipLeaves(List<PaySlipLeave> paySlipLeave);
		void InsertPaySlipComponents(List<PaySlipComponent> paySlipComponents);
	}
}
