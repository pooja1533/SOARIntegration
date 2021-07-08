using System.Collections.Generic;
using SOARIntegration.Xero.Common.Model;
namespace SOARIntegration.Xero.Api.Currencies.Service
{
	public interface ICurrenciesService
	{
        void InsertCurrencies(List<Currency> currencies);
        IEnumerable<Currency> GetCurrencies();
	}
}
