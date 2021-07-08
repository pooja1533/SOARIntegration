using System.Collections.Generic;
using SOAR.Shared.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Payments.Service
{
	public interface IPaymentsService
    {
        void InsertPayments(List<Payment> payments);

		IEnumerable<Payment> GetAllPayments();
	}
}
