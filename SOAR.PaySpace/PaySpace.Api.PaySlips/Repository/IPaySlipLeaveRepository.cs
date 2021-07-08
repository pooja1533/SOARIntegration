using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System.Collections.Generic;

namespace SOARIntegration.PaySpace.Api.PaySlips.Repository
{
	public interface IPaySlipLeaveRepository<T> where T : PaySlipLeave
	{
		IEnumerable<PaySlipLeave> GetRecordsOfCompanyByPaySlipDate(string paySlipDate, string companyCode);
	}
}
