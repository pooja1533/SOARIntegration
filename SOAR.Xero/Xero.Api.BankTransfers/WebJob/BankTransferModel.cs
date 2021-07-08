using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SOAR.Shared.Extensions;
using SOARIntegration.Xero.Api.BankTransfers.Repository;
using SOARIntegration.Xero.Api.BankTransfers.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.Core;
using Xero.Api.Core.Model;

namespace SOARIntegration.Xero.Api.BankTransfers.WebJob
{
	public class BankTransferModel
	{
		#region Private Fields
		private readonly ILogger<BankTransferModel> logger;
		private readonly IBankTransferService service;
		private readonly IXeroCoreApi api;
		private readonly Context context;
		private readonly IConfigurationRoot config;
		#endregion

		#region CTOR
		public BankTransferModel(ILogger<BankTransferModel> logger, IBankTransferService svc, Context ctx, String orgKey, IConfigurationRoot cfg)
		{
			this.logger = logger;
			this.service = svc;
			this.context = ctx;
			this.config = cfg;
			api = SOARIntegration.Xero.Common.Helpers.Application.Initialise(orgKey);
		}
		#endregion

		public void Import()
		{
			Task.Run(() => this.ProcessDataAsync()).Wait();
		}

		public async System.Threading.Tasks.Task ProcessDataAsync()
		{
			try
			{
				logger.LogInformation("Running BankTransfer web job on {0}", DateTime.Now.ToString());

				string org = config.GetValue<string>("XeroApi:Org");
				List<BankTransfer> bankTransferAsList = new List<BankTransfer>();

				int windowPeriodInYears = config.GetValue<int>("XeroApi:WindowPeriodInYears");
				var bankTransfersTemp = await api.BankTransfers.ModifiedSince(DateTime.Now.AddYears(-1 * windowPeriodInYears)).FindAsync();
				bankTransferAsList = bankTransferAsList.Concat(bankTransfersTemp).ToList();
				await Task.Delay(2000);

				List<SOARIntegration.Xero.Common.Model.BankTransfer> bankTransferList = new List<SOARIntegration.Xero.Common.Model.BankTransfer>();
				for (var count = 0; count < bankTransferAsList.Count(); count++)
				{
					var bankTransfer = bankTransferAsList[count];
					bankTransferList.Add(MapResponseData(bankTransfer, org));
				}

				service.InsertBankTRansfers(bankTransferList);
				logger.LogInformation("Total banktransfers are {0}", bankTransferList.Count());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.PrintDetails());
			}

		}

		private SOARIntegration.Xero.Common.Model.BankTransfer MapResponseData(BankTransfer bankTransfer, string orgName)
		{
			SOARIntegration.Xero.Common.Model.BankTransfer _bankTransfer = new SOARIntegration.Xero.Common.Model.BankTransfer
			{
				Id = bankTransfer.Id,
				OrgName = orgName,
				TransferDate = bankTransfer.Date,
				FromBankTransactionId = bankTransfer.FromBankTransactionId,
				ToBankTransactionId = bankTransfer.ToBankTransactionId,
				Amount = bankTransfer.Amount,
				FromBankAccountId = bankTransfer.FromBankAccount.Id,
				FromBankAccountName = bankTransfer.FromBankAccount.Name,
				FromBankAccountCode = bankTransfer.FromBankAccount.Code,
				ToBankAccountId = bankTransfer.ToBankAccount.Id,
				ToBankAccountName = bankTransfer.ToBankAccount.Name,
				ToBankAccountCode = bankTransfer.ToBankAccount.Code,
				CurrencyRate = bankTransfer.CurrencyRate,
				HasAttachments = bankTransfer.HasAttachments,
				Audit_Created = DateTime.UtcNow
			};

			return _bankTransfer;
		}
	}
}
