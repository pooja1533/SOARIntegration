using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SOAR.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.BankTransactions.Repository;
using Xero.Api.Core;
using Xero.Api.Core.Model;
using static SOAR.Shared.Xero.Common.Model.Mode;

namespace SOARIntegration.Xero.Api.BankTransactions.WebJob
{
	public class BankTransactionsModel
	{
		#region Private Fields
		private readonly ILogger logger;
		private readonly IXeroCoreApi api;
		private readonly Context context;
		private readonly IConfigurationRoot config;
		private SyncMode syncMode;
		#endregion

		#region CTOR
		public BankTransactionsModel(ILogger<BankTransactionsModel> logger, Context ctx, string orgKey, IConfigurationRoot cfg, string syncMode)
		{
			this.logger = logger;
			this.context = ctx;
			this.config = cfg;
			api = Common.Helpers.Application.Initialise(orgKey);
			this.syncMode = syncMode == "-f" ? SyncMode.Full : SyncMode.Delta;
			this.context.Database.SetCommandTimeout(3600);
		}
		#endregion

		#region Private Methods
		public void ProcessData()
		{
			Task.Run(() => this.ProcessDataAsync()).Wait();
		}
		#endregion

		#region Private Methods
		private async Task ProcessDataAsync()
		{
			try
			{
				this.logger.LogInformation("Running Bank Transaction web job on {0}", DateTime.Now.ToString());

				int count = 0;
				var lineItemCount = 0;
				string org = config.GetValue<string>("XeroApi:Org");
				int windowPeriodInYears = this.config.GetValue<int>("XeroApi:WindowPeriodInYears");
				int deltaWindowPeriodInDays = this.config.GetValue<int>("XeroApi:DeltaWindowPeriodInDays");
				DateTime windowStartDate = this.syncMode == SyncMode.Full ? DateTime.Now.AddYears(-1 * windowPeriodInYears) : DateTime.Now.AddDays(deltaWindowPeriodInDays);

				IEnumerable<BankTransaction> bankTransactions = new List<BankTransaction>();

				int page = 1;

				this.logger.LogInformation($"Fetching bank transaction page ({page})...");
				var bankTransactionsTemp = await api.BankTransactions
					.ModifiedSince(DateTime.Now.AddYears(-1 * windowPeriodInYears))
					.Page(page)
					.FindAsync();

				while (bankTransactionsTemp.Count() > 0)
				{
					bankTransactions = bankTransactions.Concat(bankTransactionsTemp);
					page++;
					logger.LogInformation($"Fetching bank transaction page ({page})...");
					bankTransactionsTemp = await api.BankTransactions
					   .ModifiedSince(new DateTime(2015, 1, 1))
					   .Page(page)
					   .FindAsync();
					await Task.Delay(2000);
				}

				var existing = await this.context.BankTransactions.Where(x => x.OrgName == org).ToArrayAsync();
				var existingIds = this.syncMode == SyncMode.Full ? existing.Select(x => x.Id) : existing.Select(x => x.Id).Intersect(bankTransactions.Select(x => x.Id));

				var existingLineItems = await this.context.BankTransactionLineItems.Where(l => existingIds.Contains(l.ReferenceId)).ToArrayAsync();
				var existingLineItemIds = await this.context.BankTransactionLineItems.Select(l => l.LineItemId).ToArrayAsync();
				var existingInvoiceLineItemTracking = await this.context.BankTransactionLineItemTrackingCategories.Where(l => existingLineItemIds.Contains(l.LineItemId)).ToArrayAsync();

				var existingTrackingCategories = await this.context.BankTransactionTrackingCategories.Where(x => x.OrgName == org).ToArrayAsync();
				var existingTrackingCategoryIds = await this.context.BankTransactionTrackingCategories.Select(l => l.Id).ToArrayAsync();
				var existingTrackingOptions = await this.context.BankTransactionTrackingOptions.Where(l => existingTrackingCategoryIds.Contains(l.TrackingCategoryId)).ToArrayAsync();

				this.context.BankTransactions.RemoveRange(existing.Where(x => existingIds.Contains(x.Id)));
				this.context.BankTransactionLineItems.RemoveRange(existingLineItems);
				this.context.BankTransactionLineItemTrackingCategories.RemoveRange(existingInvoiceLineItemTracking);
				this.context.BankTransactionTrackingCategories.RemoveRange(existingTrackingCategories);
				this.context.BankTransactionTrackingOptions.RemoveRange(existingTrackingOptions);

				await this.context.SaveChangesAsync();

				int bankTransactionsCount = bankTransactions.Count();

				List<SOAR.Shared.Xero.Common.Model.BankTransactionTrackingCategory> bankTransactionTrackingCategories = new List<SOAR.Shared.Xero.Common.Model.BankTransactionTrackingCategory>();
				List<SOAR.Shared.Xero.Common.Model.BankTransactionTrackingOption> bankTransactionTrackingOptions = new List<SOAR.Shared.Xero.Common.Model.BankTransactionTrackingOption>();

				foreach (var bankTransaction in bankTransactions)
				{
					this.logger.LogInformation($"Processing bank transaction ({count} of {bankTransactionsCount})...");

					var bnkt = new SOARIntegration.SOAR.Shared.Xero.Common.Model.BankTransaction()
					{
						Id = bankTransaction.Id,
						OrgName = org,
						ContactId = bankTransaction.Contact != null ? bankTransaction.Contact.Id : (Guid?)null,
						ContactName = bankTransaction.Contact != null ? bankTransaction.Contact.Name : null,
						Date = bankTransaction.Date,
						Status = bankTransaction.Status.ToString(),
						LineAmountTypes = bankTransaction.LineAmountTypes.ToString(),
						SubTotal = bankTransaction.SubTotal,
						TotalTax = bankTransaction.TotalTax,
						Total = bankTransaction.Total,
						CurrencyCode = bankTransaction.CurrencyCode,
						AccountId = bankTransaction.BankAccount != null ? bankTransaction.BankAccount.Id : (Guid?)null,
						AccountName = bankTransaction.BankAccount != null ? bankTransaction.BankAccount.Name : null,
						AccountCode = bankTransaction.BankAccount != null ? bankTransaction.BankAccount.Code : null,
						Type = bankTransaction.Type.ToString(),
						Reference = bankTransaction.Reference,
						IsReconciled = bankTransaction.IsReconciled.ToString(),
						Audit_Created = GetDefaultDate(DateTime.UtcNow),
					};

					if (!this.context.BankTransactions.Contains(bnkt))
					{
						this.context.BankTransactions.Add(bnkt);
					}

					if (bankTransaction.LineItems != null)
					{
						foreach (var lineItem in bankTransaction.LineItems)
						{
							try
							{
								var lItem = this.GetLineItem(lineItem, bankTransaction.Id);
								if (!this.context.BankTransactionLineItems.Contains(lItem))
								{
									this.context.BankTransactionLineItems.Add(lItem);
									lineItemCount++;
								}

								foreach (var tracking in lineItem.Tracking)
								{
									var bankTransactionLineItemTrackingCategory = this.GetBankTrnsactionLineItemTrackingCategory(lItem.LineItemId, tracking.Id, tracking.Option);
									var bankTransactionTrackingCategory = this.GetBankTransctionTrackingCategory(tracking.Id, tracking.Name, org);
									var bankTransactionTrackingOption = this.GetBankTransactionTrackingOption(tracking.Option, tracking.Id, org);
									if (!this.context.BankTransactionLineItemTrackingCategories.Contains(bankTransactionLineItemTrackingCategory))
									{
										this.context.BankTransactionLineItemTrackingCategories.Add(bankTransactionLineItemTrackingCategory);
									}

									if (!bankTransactionTrackingCategories.Any(o => o.Id == bankTransactionTrackingCategory.Id && o.OrgName == org))
									{
										bankTransactionTrackingCategories.Add(bankTransactionTrackingCategory);
										this.context.BankTransactionTrackingCategories.Add(bankTransactionTrackingCategory);
									}

									if (!bankTransactionTrackingOptions.Any(o => o.TrackingCategoryId == bankTransactionTrackingOption.TrackingCategoryId && o.Name == bankTransactionTrackingOption.Name && o.OrgName == org))
									{
										bankTransactionTrackingOptions.Add(bankTransactionTrackingOption);
										this.context.BankTransactionTrackingOptions.Add(bankTransactionTrackingOption);
									}
								}
							}
							catch (Exception lineItemEx)
							{
								this.logger.LogWarning($"Error occured while processing lineItem (ID: {lineItem.LineItemId}) for invoice (count: {count}): {lineItemEx.Message} --> {lineItemEx}");
							}
						}
					}

					count++;
				}

				await this.context.SaveChangesAsync();
				this.logger.LogInformation($"Imported {count} banktransaction(s)");
				this.logger.LogInformation($"Imported {lineItemCount} line item(s)");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.PrintDetails());
			}

		}

		private SOAR.Shared.Xero.Common.Model.BankTransactionLineItem GetLineItem(LineItem lineItem, Guid referenceId)
		{
			return new SOAR.Shared.Xero.Common.Model.BankTransactionLineItem()
			{
				LineItemId = lineItem.LineItemId,
				ReferenceId = referenceId,
				Description = lineItem.Description,
				Quantity = lineItem.Quantity,
				UnitAmount = lineItem.UnitAmount,
				AccountCode = lineItem.AccountCode,
				ItemCode = lineItem.ItemCode,
				TaxType = lineItem.TaxType,
				TaxAmount = lineItem.TaxAmount,
				LineAmount = lineItem.LineAmount,
				DiscountRate = lineItem.DiscountRate
			};
		}

		private SOAR.Shared.Xero.Common.Model.BankTransactionLineItemTrackingCategory GetBankTrnsactionLineItemTrackingCategory(Guid lineItemId, Guid trackingCategoryId, string option)
		{
			return new SOAR.Shared.Xero.Common.Model.BankTransactionLineItemTrackingCategory()
			{
				LineItemId = lineItemId,
				TrackingCategoryId = trackingCategoryId,
				Option = option
			};
		}

		private SOAR.Shared.Xero.Common.Model.BankTransactionTrackingCategory GetBankTransctionTrackingCategory(Guid trackingCategoryId, string name, string orgName)
		{
			return new SOAR.Shared.Xero.Common.Model.BankTransactionTrackingCategory()
			{
				Id = trackingCategoryId,
				Name = name,
				Status = "Active",
				OrgName = orgName,
				Audit_Created = DateTime.UtcNow
			};
		}

		private SOAR.Shared.Xero.Common.Model.BankTransactionTrackingOption GetBankTransactionTrackingOption(string option, Guid trackingCategoryId, string orgName)
		{
			return new SOAR.Shared.Xero.Common.Model.BankTransactionTrackingOption()
			{
				Id = new Guid(),
				Name = option,
				Status = "Active",
				TrackingCategoryId = trackingCategoryId,
				OrgName = orgName,
				Audit_Created = DateTime.UtcNow
			};
		}

		private DateTime GetDefaultDate(DateTime date)
		{
			return date == DateTime.MinValue ? DateTime.Now : date;
		} 
		#endregion
	}
}