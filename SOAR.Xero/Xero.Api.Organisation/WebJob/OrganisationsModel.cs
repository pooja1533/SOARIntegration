using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SOARIntegration.Xero.Api.Organisations.Service;
using Xero.Api.Core.Model;
using Xero.Api.Core;
using Microsoft.Extensions.Configuration;

namespace SOARIntegration.Xero.Api.Organisations.WebJob {
	public class OrganisationsModel
    {
		private readonly ILogger<OrganisationsModel> logger;
		private readonly IOrganisationsService _service;
        private readonly IXeroCoreApi api;
        private readonly IConfigurationRoot config;

        public OrganisationsModel(ILogger<OrganisationsModel> logger, IOrganisationsService service, string orgKey, IConfigurationRoot cfg) {
			this.logger = logger;
			this._service = service;
            api = SOARIntegration.Xero.Common.Helpers.Application.Initialise(orgKey);
            this.config = cfg;
        }

        public void ProcessData () {
            logger.LogInformation("Running Organisations web job on {0}", DateTime.Now.ToString());

            string orgName = config.GetValue<string>("XeroApi:Org");
            var accounts = api.Organisations.FindAsync();

            var accountsAsList = accounts.Result.ToList();
            List<SOARIntegration.Xero.Common.Model.Organisation> organisationList = new List<SOARIntegration.Xero.Common.Model.Organisation>();
            for (var count = 0; count < accountsAsList.Count(); count++)
            {
                var org = accountsAsList[count];
                organisationList.Add(MapResponseData(org));
                logger.LogInformation("Organisation Name {0}", org.ShortCode);
            }
            _service.InsertOrganisations(organisationList);
            logger.LogInformation("Total Organisations are {0}", accountsAsList.Count());
        }

        private SOARIntegration.Xero.Common.Model.Organisation MapResponseData(Organisation organisation)
        {

            SOARIntegration.Xero.Common.Model.Organisation _organisation = new SOARIntegration.Xero.Common.Model.Organisation
            {
                OrganisationId = organisation.Id.ToString(),
                Name = organisation.Name,
                LegalName = organisation.LegalName,
                ShortCode = organisation.ShortCode,
                PaysTax = organisation.PaysTax,
                Version = organisation.Version.GetEnumMemberValue(),
                OrganisationType = organisation.OrganisationType.GetEnumMemberValue(),
                IsDemoCompany = organisation.IsDemoCompany,
                OrganisationStatus = organisation.OrganisationStatus.GetEnumMemberValue(),
                BaseCurrency = organisation.BaseCurrency,
                ApiKey = organisation.ApiKey,
                CountryCode = organisation.CountryCode,
                TaxNumber = organisation.TaxNumber,
                FinancialYearEndDay = organisation.FinancialYearEndDay,
                FinancialYearEndMonth = organisation.FinancialYearEndMonth,
                PeriodLockDate = organisation.PeriodLockDate,
                EndOfYearLockDate = organisation.EndOfYearLockDate,
                CreatedDateUtc = organisation.CreatedDateUtc,
                Timezone = organisation.Timezone,
                LineOfBusiness = organisation.LineOfBusiness,
                RegistrationNumber = organisation.RegistrationNumber,
                SalesTaxPeriod = organisation.SalesTaxPeriod.GetEnumMemberValue(),
                SalesTaxBasisType = organisation.SalesTaxBasisType.GetEnumMemberValue()
            };

            if(organisation.Addresses.Count > 0)
            {
                _organisation.Addresses = new List<Common.Model.OrganisationAddress>();
                foreach (Address address in organisation.Addresses)
                {
                    _organisation.Addresses.Add(new SOARIntegration.Xero.Common.Model.OrganisationAddress
                    {
                        AddressType = address.AddressType.GetEnumMemberValue(),
                        AddressLine1 = address.AddressLine1,
                        AddressLine2 = address.AddressLine2,
                        AddressLine3 = address.AddressLine3,
                        AddressLine4 = address.AddressLine4,
                        City = address.City,
                        Region = address.Region,
                        PostalCode = address.PostalCode,
                        Country = address.Country,
                        AttentionTo = address.AttentionTo,
                        Created = GetDefaultDate(DateTime.Now),
                        Modified = GetDefaultDate(DateTime.Now),
                    });
                }
            }
            if (organisation.Phones.Count > 0)
            {
                _organisation.Phones = new List<Common.Model.OrganisationPhone>();
                foreach (Phone address in organisation.Phones)
                {
                    _organisation.Phones.Add(new SOARIntegration.Xero.Common.Model.OrganisationPhone
                    {
                        PhoneType = address.PhoneType.GetEnumMemberValue(),
                        PhoneNumber = address.PhoneNumber,
                        PhoneAreaCode = address.PhoneAreaCode,
                        PhoneCountryCode = address.PhoneCountryCode,
                        Created = GetDefaultDate(DateTime.Now),
                        Modified = GetDefaultDate(DateTime.Now),
                    });
                }
            }
            return _organisation;

        }
        private DateTime GetDefaultDate(DateTime date)
        {
            return date == DateTime.MinValue ? DateTime.Now : date;
        }
    }
}