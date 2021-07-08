using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SOARIntegration.Xero.Api.Currencies.Service;
using Xero.Api.Core.Model;
using Xero.Api.Core;
using Microsoft.Extensions.Configuration;

namespace SOARIntegration.Xero.Api.Currencies.WebJob {
	public class CurrenciesModel
    {
		private readonly ILogger<CurrenciesModel> logger;
		private readonly ICurrenciesService _service;
        private readonly IXeroCoreApi api;
        private readonly IConfiguration config;

        public CurrenciesModel(ILogger<CurrenciesModel> logger, ICurrenciesService service, string orgKey, IConfiguration configuration) {
			this.logger = logger;
			this._service = service;
            this.config = configuration;
            api = SOARIntegration.Xero.Common.Helpers.Application.Initialise(orgKey);
        }

        public void ProcessData () {
            try{
                System.Console.WriteLine("Running Currencies web job on {0}", DateTime.Now.ToString());

                string orgName = config.GetValue<string>("XeroApi:Org");
                var response = api.Currencies.FindAsync();

                var responseAsList = response.Result.ToList();
                List<SOARIntegration.Xero.Common.Model.Currency> currenciesList = new List<SOARIntegration.Xero.Common.Model.Currency>();
                for (var count = 0; count < responseAsList.Count(); count++)
                {
                    var resp = responseAsList[count];
                    currenciesList.Add(MapResponseData(resp, orgName));
                    System.Console.WriteLine("Currency Code {0} - Currenct Description {1}", resp.Code, resp.Description);
                }
                _service.InsertCurrencies(currenciesList);
                System.Console.WriteLine("Total Currencies are {0}", responseAsList.Count());
            } catch(Exception e){
                System.Console.WriteLine(e.Message);
            }

        }

        private SOARIntegration.Xero.Common.Model.Currency MapResponseData(Currency currency, string orgName)
        {
            SOARIntegration.Xero.Common.Model.Currency _currency = new SOARIntegration.Xero.Common.Model.Currency
            {
                Code = currency.Code,
                OrgName = orgName,
                Description = currency.Description
            };

            return _currency;

        }
    }
}