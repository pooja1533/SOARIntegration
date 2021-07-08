using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SOARIntegration.Xero.Api.ExpenseClaims.Service;
using Xero.Api.Core.Model;
using Xero.Api.Core;
using Microsoft.Extensions.Configuration;

namespace SOARIntegration.Xero.Api.ExpenseClaims.WebJob {
	public class ExpenseClaimsModel
    {
		private readonly ILogger<ExpenseClaimsModel> logger;
		private readonly IExpenseClaimsService _service;
        private readonly IXeroCoreApi api;
        private readonly IConfigurationRoot config;

        public ExpenseClaimsModel(ILogger<ExpenseClaimsModel> logger, IExpenseClaimsService service, string orgKey, IConfigurationRoot cfg) {
			this.logger = logger;
			this._service = service;
            this.config = cfg;
            api = SOARIntegration.Xero.Common.Helpers.Application.Initialise(orgKey);

        }

        public void ProcessData () {
            System.Console.WriteLine("Running ExpenseClaims web job on {0}", DateTime.Now.ToString());

            string org = config.GetValue<string>("XeroApi:Org");

            var response = api.ExpenseClaims.FindAsync();

            var responseAsList = response.Result.ToList();
            List<SOARIntegration.Xero.Common.Model.ExpenseClaim> expenseClaimsList = new List<SOARIntegration.Xero.Common.Model.ExpenseClaim>();
            for (var count = 0; count < responseAsList.Count(); count++)
            {
                var claim = responseAsList[count];
                expenseClaimsList.Add(MapResponseData(claim, org));
            }
            _service.InsertExpenseClaims(expenseClaimsList);
            System.Console.WriteLine("Total ExpenseClaims are {0}", responseAsList.Count());

        }

        private SOARIntegration.Xero.Common.Model.ExpenseClaim MapResponseData(ExpenseClaim claim, string orgName)
        {

            SOARIntegration.Xero.Common.Model.ExpenseClaim _claim = new SOARIntegration.Xero.Common.Model.ExpenseClaim
            {
                ExpenseClaimID = claim.Id.ToString(),
                OrgName = orgName,
                AmountDue = claim.AmountDue,
                AmountPaid = claim.AmountPaid,
                PaymentDueDate = claim.PaymentDueDate,
                ReportingDate = claim.ReportingDate,
                UpdatedDateUTC = claim.UpdatedDateUtc,
                UserId = claim.User.Id.ToString(),
                Total = claim.Total,
                Status = claim.Status.ToString()
            };

            return _claim;

        }
    }
}