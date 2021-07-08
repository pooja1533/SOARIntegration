using SOARIntegration.Xero.Common.Model;
using SOARIntegration.Xero.Api.Organisations.Repository;
using System.Collections.Generic;
using System;

namespace SOARIntegration.Xero.Api.Organisations.Service
{
	public class OrganisationsService : IOrganisationsService
    {
        private readonly IRepository<Organisation> _repository;

        public OrganisationsService(IRepository<Organisation> repository)
		{
			this._repository = repository;
		}

        public void InsertOrganisations(List<Organisation> organisations){
            for (var count = 0; count < organisations.Count; count++)
            {
                try
                {
                    var org = _repository.Get(organisations[count].OrganisationId);
                    if (org == null)
                    {
                        _repository.Insert(organisations[count]);
                    }
                    else
                    {
                        organisations[count].Id = org.Id;
                        organisations[count].Created = org.Created;

                        if (organisations[count].Addresses != null)
                            foreach (OrganisationAddress item in organisations[count].Addresses)
                            {
                                OrganisationAddress existingItem = org.Addresses.Find(m => m.AddressType.Equals(item.AddressType));
                                if (existingItem != null)
                                {
                                    item.Id = existingItem.Id;
                                }
                                 item.OrganisationId = org.Id;

                            }

                        if(organisations[count].Phones != null)
                            foreach (OrganisationPhone item in organisations[count].Phones)
                            {
                                OrganisationPhone existingItem = org.Phones.Find(m => m.PhoneType.Equals(item.PhoneType));
                                if (existingItem != null)
                                {
                                    item.Id = existingItem.Id;
                                }
                                item.OrganisationId = org.Id;

                            }
                        _repository.Update(organisations[count]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

		public IEnumerable<Organisation> GetOrganisations()
		{
			return _repository.GetAll();
		}
	}
}
