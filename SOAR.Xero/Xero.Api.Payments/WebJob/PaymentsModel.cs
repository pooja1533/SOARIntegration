
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using SOAR.Shared.Xero.Common.Model;
using Microsoft.Extensions.Logging;
using SOARIntegration.Xero.Api.Payments.Service;
using Xero.Api.Core.Model;
using Xero.Api.Core;
using Microsoft.Extensions.Configuration;
using Payment = SOAR.Shared.Xero.Common.Model.Payment;

namespace SOARIntegration.Xero.Api.Payments.WebJob
{
    public class PaymentsModel
    {
        private readonly ILogger<PaymentsModel> logger;
        private readonly IPaymentsService _service;
        private readonly IXeroCoreApi api;
        private readonly IConfigurationRoot config;

        public PaymentsModel(ILogger<PaymentsModel> logger, IPaymentsService service, string orgKey, IConfigurationRoot cfg)
        {
            this.logger = logger;
            this._service = service;
            api = SOARIntegration.Xero.Common.Helpers.Application.Initialise(orgKey);
            this.config = cfg;
        }

        public void ProcessData()
        {
            logger.LogInformation("Running Payments web job on {0}", DateTime.Now.ToString());

            string org = config.GetValue<string>("XeroApi:Org");
            var payments = api.Payments.FindAsync();

            var paymentsAsList = payments.Result.ToList();
            List<Payment> paymentList = new List<Payment> ();
            for (var count = 0; count < paymentsAsList.Count(); count++)
            {
                var pm = paymentsAsList[count];
                paymentList.Add(MapResponseData(pm, org));
                logger.LogInformation("Payments Amount {0}", pm.Amount);
            }
            _service.InsertPayments(paymentList);
            logger.LogInformation("Total Payments are {0}", paymentsAsList.Count());

        }
        private Payment MapResponseData(global::Xero.Api.Core.Model.Payment payment, string orgName)
        {

            Payment _payment = new Payment
            {
                Id = payment.Id,
                OrgName = orgName,
                Type = payment.Type.GetEnumMemberValue(),
                Status = payment.Status.GetEnumMemberValue(),
                Date = payment.Date,
                CurrencyRate = payment.CurrencyRate,
                BankAmount = payment.BankAmount,
                Amount = payment.Amount,
                Reference = payment.Reference,
                IsReconciled = payment.IsReconciled,
                InvoiceId = payment.Invoice?.Id,
                CreditNoteId = payment.CreditNote?.Id,
                PrepaymentId = payment.Prepayment?.Id,
                OverpaymentId = payment.Overpayment?.Id,
                AccountId = payment.Account?.Id
            };

            return _payment;
        }
    }
}