using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaySpace.Api.PaySlips.Repository
{
	public interface IPaySlipComponentRepository<T> where T : PaySlipComponent
	{
		IEnumerable<PaySlipComponent> GetRecordsOfCompanyByPaySlipDate(string paySlipDate, string companyCode);
	}
}
