using SOARIntegration.Xero.Common.Model;
using SOARIntegration.Xero.Api.Currencies.Repository;
using System.Collections.Generic;
using System;

namespace SOARIntegration.Xero.Api.Currencies.Service
{
	public class CurrenciesService : ICurrenciesService
    {
        private IRepository<Currency> _repository;

        public CurrenciesService(IRepository<Currency> repository)
		{
			this._repository = repository;
		}

        public void InsertCurrencies(List<Currency> currencies)
        {
            for (var count = 0; count < currencies.Count; count++)
            {
                try
                {
                    var currency = _repository.Get(currencies[count].Code);
                    if (currency == null)
                    {
                        _repository.Insert(currencies[count]);
                    }
                    else
                    {
                        currencies[count].Id = currency.Id;
                        currencies[count].Created = currency.Created;
                        _repository.Update(currencies[count]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<Currency> GetCurrencies()
		{
			return _repository.GetAll();
		}
	}
}
