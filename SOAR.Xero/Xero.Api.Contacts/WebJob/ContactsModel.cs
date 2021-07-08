using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Xero.Api.Core.Model;
using Xero.Api.Core;
using SOAR.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using SOARIntegration.Xero.Api.Contacts.Service;
using Xero.Api.Contacts.Repository;
using System.Threading.Tasks;

namespace SOARIntegration.Xero.Api.Contacts.WebJob {
    public class ContactsModel {

        private readonly ILogger<ContactsModel> logger;
        private readonly IContactService service;
        private readonly IXeroCoreApi api;
        private readonly Context context;
        private readonly IConfigurationRoot config;

        public ContactsModel(ILogger<ContactsModel> logger, IContactService svc, Context ctx, String orgKey, IConfigurationRoot cfg) {
            this.logger = logger;
            this.service = svc;
            this.context = ctx;
            this.config = cfg;
            api = SOARIntegration.Xero.Common.Helpers.Application.Initialise(orgKey);
        }

        public void Import()
        {
            Task.Run(() => this.ProcessDataAsync()).Wait();
        }

        public async System.Threading.Tasks.Task ProcessDataAsync() {
            try
            {
                logger.LogInformation("Running Contact web job on {0}", DateTime.Now.ToString());

                string org = config.GetValue<string>("XeroApi:Org");
                List<Contact> contactsAsList = new List<Contact>();

                int page = 1;
                int windowPeriodInYears = config.GetValue<int>("XeroApi:WindowPeriodInYears");

                logger.LogInformation($"Fetching contacts page ({page})...");
                var contactsTemp = await api.Contacts.Page(page).FindAsync();

                while (contactsTemp.Count() > 0)
                {
                    contactsAsList = contactsAsList.Concat(contactsTemp).ToList();
                    page++;
                    logger.LogInformation($"Fetching contacts page ({page})...");
                    contactsTemp = await api.Contacts
                       .ModifiedSince(DateTime.Now.AddYears(-1 * windowPeriodInYears))
                       .Page(page)
                       .FindAsync();
                    await Task.Delay(2000);
                }
               
                List<SOARIntegration.Xero.Common.Model.Contact> contactList = new List<SOARIntegration.Xero.Common.Model.Contact>();
                for (var count = 0; count < contactsAsList.Count(); count++)
                {
                    var contact = contactsAsList[count];
                    contactList.Add(MapResponseData(contact, org));
                    logger.LogInformation("Contact Name {0}", contact.Name);
                }
                service.InsertContacts(contactList);
                logger.LogInformation("Total Contacts are {0}", contactsAsList.Count());
            } catch(Exception ex)
            {
                Console.WriteLine(ex.PrintDetails());
            }

        }

        private SOARIntegration.Xero.Common.Model.Contact MapResponseData(Contact contact, string orgName)
        {

            SOARIntegration.Xero.Common.Model.Contact _contact = new SOARIntegration.Xero.Common.Model.Contact
            {
                ContactID = contact.Id.ToString(),
                OrgName = orgName,
                ContactStatus = contact.ContactStatus.GetEnumMemberValue(),
                Name = contact.Name,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                ContactNumber = contact.ContactNumber,
                EmailAddress = contact.EmailAddress,
                SkypeUserName = contact.SkypeUserName,
                BankAccountDetails = contact.BankAccountDetails,
                TaxNumber = contact.TaxNumber,
                AccountsReceivableTaxType = contact.AccountsReceivableTaxType,
                AccountsPayableTaxType = contact.AccountsPayableTaxType,
                DefaultCurrency = contact.DefaultCurrency,
                Website = contact.Website,
                XeroNetworkKey = contact.XeroNetworkKey,
                PurchaseAccountCode = contact.PurchaseAccountCode,
                SalesAccountCode = contact.SalesAccountCode,
                AccountNumber = contact.AccountNumber,
                Discount = contact.Discount,
                IsSupplier = (bool)(contact.IsSupplier.HasValue ? contact.IsSupplier : false),
                IsCustomer = (bool)(contact.IsCustomer.HasValue ? contact.IsCustomer : false),
                HasAttachments = (bool)(contact.HasAttachments.HasValue ? contact.HasAttachments : false),
                Created = GetDefaultDate(DateTime.Now),
                Modified = GetDefaultDate(DateTime.Now)
            };
            return _contact;
        }

        private DateTime GetDefaultDate(DateTime date)
        {
            return date == DateTime.MinValue ? DateTime.Now : date;
        }
    }
}