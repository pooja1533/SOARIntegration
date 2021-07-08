using System.Collections.Generic;
using SOARIntegration.Xero.Common.Model;
namespace SOARIntegration.Xero.Api.Organisations.Service
{
	public interface IOrganisationsService
    {
        void InsertOrganisations(List<Organisation> organisations);

		IEnumerable<Organisation> GetOrganisations();
	}
}
