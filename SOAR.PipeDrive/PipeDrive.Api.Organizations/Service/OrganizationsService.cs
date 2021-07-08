#region NameSpace
using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Organizations.Repository;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Organizations.Service
{
    public class OrganizationsService : IOrganizationsService
    {
		#region Private Fields
		private IRepository<Organization> _repository;
		#endregion

		#region CTOR
		public OrganizationsService(IRepository<Organization> repository)
		{
			this._repository = repository;
		}
		#endregion

		#region InsertOrganizations
		public void InsertOrganizations(List<Organization> organizations)
		{
			for (var count = 0; count < organizations.Count; count++)
			{
				try
				{
					var organization = _repository.Get(organizations[count].EntityId);
					if (organization == null)
					{
						_repository.Insert(organizations[count]);
					}
					else
					{
						organizations[count].Id = organization.Id;
						organizations[count].Audit_Created = organization.Audit_Created;
						organizations[count].Audit_Modified = organization.Audit_Modified;
						organizations[count].Audit_Deleted = organization.Audit_Deleted;
						_repository.Update(organizations[count]);
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		} 
		#endregion
	}
}
