using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Organizations.Service;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Organizations.WebJob
{
    public class OrganizationModel
    {
        #region Private Fields
        private JSONToObject _organizationList;
        #endregion

        #region CTOR
        public OrganizationModel(JSONToObject organization)
        {
			_organizationList = organization;
        }
        #endregion

        #region ProcessData
        public void ProcessData(IOrganizationsService organizationsService)
        {
            try
            {
                List<Organization> organizationList = new List<Organization>();
                foreach (var organization in _organizationList.Data)
                {
                    var objDeal = MapFields(organization);
                    organizationList.Add(objDeal);
                }

                organizationsService.InsertOrganizations(organizationList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Map Fields
        private Organization MapFields(OrganizationData organization)
        {
            Organization objOrganization = new Organization();
            objOrganization.EntityId = organization.Id.ToString();
            objOrganization.Name = organization.Name;
            objOrganization.Open_Deals_Count = organization.OpenDealsCount;
            objOrganization.Related_Open_Deals_Count = organization.RelatedOpenDealsCount;
            objOrganization.Closed_Deals_Count = organization.ClosedDealsCount;
            objOrganization.Related_Closed_Deals_Count = organization.RelatedClosedDealsCount;
            objOrganization.Email_Messages_Count = organization.EmailMessagesCount;
            objOrganization.People_Count = organization.PeopleCount;
            objOrganization.Activities_Count = organization.ActivitiesCount;
			objOrganization.Done_Activities_Count = organization.DoneActivitiesCount;
			objOrganization.UnDone_Activities_Count = organization.UndoneActivitiesCount;
			objOrganization.Reference_Activities_Count = organization.ReferenceActivitiesCount;
			objOrganization.Files_Count = organization.FilesCount;
			objOrganization.Notes_Count = organization.NotesCount;
			objOrganization.Followers_Count = organization.FollowersCount;
			objOrganization.Won_Deals_Count = organization.WonDealsCount;
			objOrganization.Related_Won_Deals_Count = organization.RelatedWonDealsCount;
			objOrganization.Lost_Deals_Count = organization.LostDealsCount;
			objOrganization.Related_Lost_Deals_Count = organization.RelatedLostDealsCount;
			objOrganization.Active_Flag = organization.ActiveFlag;
			objOrganization.Category_Id = organization.CategoryId;
			objOrganization.Picture_Id = organization.PictureId;
			objOrganization.Country_Code = organization.CountryCode;
			objOrganization.First_Char = organization.FirstChar;
			objOrganization.Update_Time = organization.UpdateTime;
			objOrganization.Add_Time = organization.AddTime;
			objOrganization.Visible_To = organization.VisibleTo;
			objOrganization.Next_Activity_Date = organization.NextActivityDate;
			objOrganization.Next_Activity_Time = organization.NextActivityTime;
			objOrganization.Next_Activity_Id = organization.NextActivityId;
			objOrganization.Last_Activity_Id = organization.LastActivityId;
			objOrganization.Last_Activity_Date = organization.LastActivityDate;

			objOrganization.Address = organization.Address;
			objOrganization.Address_Subpremise = organization.AddressSubpremise;
			objOrganization.Address_Street_Number = organization.AddressStreetNumber;
			objOrganization.Address_Route = organization.AddressRoute;
			objOrganization.Address_Sublocality = organization.AddressSublocality;
			objOrganization.Address_Locality = organization.AddressLocality;
			objOrganization.Address_Admin_Area_Level_1 = organization.AddressAdminAreaLevel1;
			objOrganization.Address_Admin_Area_Level_2 = organization.AddressAdminAreaLevel2;
			objOrganization.Address_Country = organization.AddressCountry;
			objOrganization.Address_Postal_Code = organization.AddressPostalCode;
			objOrganization.Address_Formatted_Address = organization.AddressFormattedAddress;
			objOrganization.Owner_Name = organization.OwnerName;
			objOrganization.Owner_Id = organization.OwnerId.Id.ToString();
			objOrganization.CC_Email = organization.CcEmail;

			return objOrganization;
        }

        #endregion
    }
}
