using System.Collections.Generic;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Organizations.Service
{
    public interface IOrganizationsService
    {
        void InsertOrganizations(List<Organization> organizations);
    }
}
