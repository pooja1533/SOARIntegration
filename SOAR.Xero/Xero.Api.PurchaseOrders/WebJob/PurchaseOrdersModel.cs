using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SOARIntegration.Xero.Api.PurchaseOrders.Service;
using Xero.Api.Core.Model;
using Xero.Api.Core;
using SOAR.Shared.Extensions;
using SOARIntegration.Xero.Api.PurchaseOrders.Repository;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace SOARIntegration.Xero.Api.PurchaseOrders.WebJob {
    public class PurchaseOrdersModel {
        private readonly ILogger<PurchaseOrdersModel> logger;
        private readonly IPurchaseOrdersService _service;
        private readonly IXeroCoreApi api;
        private readonly Context context;
        private readonly IConfigurationRoot config;

        public PurchaseOrdersModel (ILogger<PurchaseOrdersModel> logger, IPurchaseOrdersService service, Context ctx, String orgKey, IConfigurationRoot cfg) {
            this.logger = logger;
            this._service = service;
            this.context = ctx;
            this.config = cfg;
            api = SOARIntegration.Xero.Common.Helpers.Application.Initialise(orgKey);
        }

        public void ProcessData() {
            Task.Run(() => this.ProcessDataAsync()).Wait();
        }

        public async Task ProcessDataAsync() {
            try
            {
                logger.LogInformation("Running PurchaseOrders web job on {0}", DateTime.Now.ToString());

                string org = config.GetValue<string>("XeroApi:Org");
                var response = api.PurchaseOrders.FindAsync();

                int windowPeriodInYears = config.GetValue<int>("XeroApi:WindowPeriodInYears");
                IEnumerable<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
                int page = 1;

                logger.LogInformation($"Fetching purchase orders page ({page})...");
                var purchaseOrdersTemp =  await api.PurchaseOrders
                    .ModifiedSince(DateTime.Now.AddYears(-1 * windowPeriodInYears))
                    .Page(page)
                    .FindAsync();

                while (purchaseOrdersTemp.Count() > 0)
                {
                    purchaseOrders = purchaseOrders.Concat(purchaseOrdersTemp);
                    page++;
                    logger.LogInformation($"Fetching purchase order page ({page})...");
                    purchaseOrdersTemp = await api.PurchaseOrders
                       .ModifiedSince(new DateTime(2015, 1, 1))
                       .Page(page)
                       .FindAsync();
                    await Task.Delay(2000);
                }

                List<SOARIntegration.Xero.Common.Model.PurchaseOrder> purchaseOrderList = new List<SOARIntegration.Xero.Common.Model.PurchaseOrder>();
                foreach (var po in purchaseOrders)
                {
                    purchaseOrderList.Add(MapResponseData(po, org));
                    logger.LogInformation("PurchaseOrder Reference {0}", po.Reference);
                }
                _service.InsertPurchaseOrders(purchaseOrderList);
				await ProcessTrackingDataAsync(purchaseOrders, org);
                logger.LogInformation("Total PurchaseOrders are {0}", purchaseOrders.Count());
            } catch(Exception ex)
            {
                Console.WriteLine(ex.PrintDetails());
            }

        }

        private SOARIntegration.Xero.Common.Model.PurchaseOrder MapResponseData(PurchaseOrder purchaseOrder, string orgName)
        {

            SOARIntegration.Xero.Common.Model.PurchaseOrder _purchaseOrder = new SOARIntegration.Xero.Common.Model.PurchaseOrder
            {
                PurchaseOrderID = purchaseOrder.Id,
                OrgName = orgName,
                PurchaseOrderNumber = purchaseOrder.Number,
                PurchaseOrderDate = purchaseOrder.Date,
                DeliveryDate = purchaseOrder.DeliveryDate,
                ExpectedArrivalDate = purchaseOrder.ExpectedArrivalDate,
                DeliveryAddress = purchaseOrder.DeliveryAddress,
                AttentionTo = purchaseOrder.AttentionTo,
                Telephone = purchaseOrder.Telephone,
                DeliveryInstructions = purchaseOrder.DeliveryInstructions,
                SentToContact = purchaseOrder.SentToContact,
                Reference = purchaseOrder.Reference,
                CurrencyRate = purchaseOrder.CurrencyRate,
                CurrencyCode = purchaseOrder.CurrencyCode,
                Status = purchaseOrder.Status.GetEnumMemberValue(),
                SubTotal = purchaseOrder.SubTotal,
                TotalTax = purchaseOrder.TotalTax,
                Total = purchaseOrder.Total,
                TotalDiscount = purchaseOrder.TotalDiscount,
                UpdatedDateUtc = purchaseOrder.UpdatedDateUtc,
                 Created = GetDefaultDate(DateTime.Now),
                Modified = GetDefaultDate(DateTime.Now)

            };
            if (purchaseOrder.Contact != null)
            {
                _purchaseOrder.Contact = new SOARIntegration.Xero.Common.Model.PurchaseOrderContact
                {
                    ContactID = purchaseOrder.Contact.Id.ToString(),
                    ContactStatus = purchaseOrder.Contact.ContactStatus.GetEnumMemberValue(),
                    Name = purchaseOrder.Contact.Name,
                    FirstName = purchaseOrder.Contact.FirstName,
                    LastName = purchaseOrder.Contact.LastName,
                    ContactNumber = purchaseOrder.Contact.ContactNumber,
                    EmailAddress = purchaseOrder.Contact.EmailAddress,
                    SkypeUserName = purchaseOrder.Contact.SkypeUserName,
                    BankAccountDetails = purchaseOrder.Contact.BankAccountDetails,
                    TaxNumber = purchaseOrder.Contact.TaxNumber,
                    AccountsReceivableTaxType = purchaseOrder.Contact.AccountsReceivableTaxType,
                    AccountsPayableTaxType = purchaseOrder.Contact.AccountsPayableTaxType,
                    DefaultCurrency = purchaseOrder.Contact.DefaultCurrency,
                    Website = purchaseOrder.Contact.Website,
                    XeroNetworkKey = purchaseOrder.Contact.XeroNetworkKey,
                    PurchaseAccountCode = purchaseOrder.Contact.PurchaseAccountCode,
                    SalesAccountCode = purchaseOrder.Contact.SalesAccountCode,
                    AccountNumber = purchaseOrder.Contact.AccountNumber,
                    Discount = purchaseOrder.Contact.Discount,
                    IsSupplier = (bool)(purchaseOrder.Contact.IsSupplier.HasValue ? purchaseOrder.Contact.IsSupplier : false),
                    IsCustomer = (bool)(purchaseOrder.Contact.IsCustomer.HasValue ? purchaseOrder.Contact.IsCustomer : false),
                    HasAttachments = (bool)(purchaseOrder.Contact.HasAttachments.HasValue ? purchaseOrder.Contact.HasAttachments : false),
                    Created = GetDefaultDate(DateTime.Now),
                    Modified = GetDefaultDate(DateTime.Now)
                };
            }

            _purchaseOrder.LineItems = new List<Common.Model.PurchaseOrderLineItem>();

			if (purchaseOrder.LineItems.Count > 0)
			{
				_purchaseOrder.LineItems = new List<Common.Model.PurchaseOrderLineItem>();
				var index = 0;
				foreach (var item in purchaseOrder.LineItems)
				{
					_purchaseOrder.LineItems.Add(new SOARIntegration.Xero.Common.Model.PurchaseOrderLineItem
					{
						LineItemId = item.LineItemId,
						Description = item.Description,
						Quantity = item.Quantity,
						UnitAmount = item.UnitAmount,
						TaxAmount = item.TaxAmount,
						LineAmount = item.LineAmount,
						DiscountRate = item.DiscountRate,
						Created = GetDefaultDate(DateTime.Now),
						Modified = GetDefaultDate(DateTime.Now),
						//Tracking = new Common.Model.ItemTracking
						//{
						//}

					});
					foreach (ItemTrackingCategory cat in item.Tracking)
					{
						//_purchaseOrder.LineItems[index].Tracking.Add(new SOARIntegration.Xero.Common.Model.ItemTrackingCategory
						//{
						//	TrackingCategoryID = cat.Id.ToString(),
						//	Name = cat.Name,
						//	Option = cat.Option,
						//	OptionId = cat.OptionId.ToString(),
						//	Created = GetDefaultDate(DateTime.Now),
						//	Modified = GetDefaultDate(DateTime.Now)
						//});
					}
				}
			}
			return _purchaseOrder;
        }

		private async Task ProcessTrackingDataAsync(IEnumerable<PurchaseOrder> purchaseOrders, string org)
		{
			try
			{
				var count = 0;
				List<SOAR.Shared.Xero.Common.Model.PurchaseOrderTrackingCategory> purchaseOrderTrackingCategories = new List<SOAR.Shared.Xero.Common.Model.PurchaseOrderTrackingCategory>();
				List<SOAR.Shared.Xero.Common.Model.PurchaseOrderTrackingOption> purchaseOrderTrackingOptions = new List<SOAR.Shared.Xero.Common.Model.PurchaseOrderTrackingOption>();

				foreach (var purchaseOrder in purchaseOrders)
				{
					var purchaseOrderId = this.context.PurchaseOrders.Where(o => o.PurchaseOrderID == purchaseOrder.Id).Select(o => o.Id).SingleOrDefault();
					if (purchaseOrder.LineItems != null)
					{
						foreach (var lineItem in purchaseOrder.LineItems)
						{
							try
							{
								var lItem = this.GetLineItem(lineItem, purchaseOrderId);
								
								foreach (var tracking in lineItem.Tracking)
								{
									var purchaseOrderLineItemTrackingCategory = this.GetPurchaseOrderLineItemTrackingCategory(lItem.LineItemId, tracking.Id, tracking.Option);
									var purchaseOrderTrackingCategory = this.GetPurchaseOrderTrackingCategory(tracking.Id, tracking.Name, org);
									var purchaseOrderTrackingOption = this.GetPurchaseOrderTrackingOption(tracking.Option, tracking.Id, org);
									if (!this.context.PurchaseOrderLineItemTrackingCategories.Contains(purchaseOrderLineItemTrackingCategory))
									{
										this.context.PurchaseOrderLineItemTrackingCategories.Add(purchaseOrderLineItemTrackingCategory);
									}

									if (!purchaseOrderTrackingCategories.Any(o => o.Id == purchaseOrderTrackingCategory.Id && o.OrgName == org))
									{
										purchaseOrderTrackingCategories.Add(purchaseOrderTrackingCategory);
										this.context.PurchaseOrderTrackingCategories.Add(purchaseOrderTrackingCategory);
									}

									if (!purchaseOrderTrackingOptions.Any(o => o.TrackingCategoryId == purchaseOrderTrackingOption.TrackingCategoryId && o.Name == purchaseOrderTrackingOption.Name && o.OrgName == org))
									{
										purchaseOrderTrackingOptions.Add(purchaseOrderTrackingOption);
										this.context.PurchaseOrderTrackingOptions.Add(purchaseOrderTrackingOption);
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
				this.logger.LogInformation($"Imported {count} purchaseOrder(s)");
			}
			catch(Exception ex)
			{
				throw ex;
			}
			
		}

		private SOARIntegration.Xero.Common.Model.PurchaseOrderLineItem GetLineItem(LineItem lineItem, int referenceId)
		{
			return new SOARIntegration.Xero.Common.Model.PurchaseOrderLineItem()
			{
				LineItemId = lineItem.LineItemId,
				PurchaseOrderId = referenceId,
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

		private SOAR.Shared.Xero.Common.Model.PurchaseOrderLineItemTrackingCategory GetPurchaseOrderLineItemTrackingCategory(Guid lineItemId, Guid trackingCategoryId, string option)
		{
			return new SOAR.Shared.Xero.Common.Model.PurchaseOrderLineItemTrackingCategory()
			{
				LineItemId = lineItemId,
				TrackingCategoryId = trackingCategoryId,
				Option = option
			};
		}

		private SOAR.Shared.Xero.Common.Model.PurchaseOrderTrackingCategory GetPurchaseOrderTrackingCategory(Guid trackingCategoryId, string name, string orgName)
		{
			return new SOAR.Shared.Xero.Common.Model.PurchaseOrderTrackingCategory()
			{
				Id = trackingCategoryId,
				Name = name,
				Status = "Active",
				OrgName = orgName,
				Audit_Created = DateTime.UtcNow
			};
		}

		private SOAR.Shared.Xero.Common.Model.PurchaseOrderTrackingOption GetPurchaseOrderTrackingOption(string option, Guid trackingCategoryId, string orgName)
		{
			return new SOAR.Shared.Xero.Common.Model.PurchaseOrderTrackingOption()
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
    }
}