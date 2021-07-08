#region NameSpaces
using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Deals.Service;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

#endregion
namespace SOARIntegration.PipeDrive.Api.Deals.WebJob
{
    public class DealModel
	{
		#region Private Fields
		private JSONToObject _deal;
		#endregion

		#region CTOR
		public DealModel(JSONToObject deal)
		{
			_deal = deal;
		}
		#endregion

		#region ProcessData
		public void ProcessData(IDealsService dealService)
		{
			try
			{
				List<Deal> dealList = new List<Deal>();
				foreach (var deal in _deal.Data)
				{
					var objDeal = MapFields(deal);
					dealList.Add(objDeal);
				}

				dealService.InsertDeals(dealList);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Map Fields
		private Deal MapFields(DealData deal)
		{
			Deal objDeal = new Deal();
			objDeal.EntityId = deal.Id.ToString();
			objDeal.Stage_Id = deal.StageId;
			objDeal.Title = deal.Title;
			objDeal.Value = deal.Value;
			objDeal.Currency = deal.Currency;
			objDeal.Add_Time = deal.AddTime;
			objDeal.Update_Time = deal.UpdateTime;
			objDeal.Stage_Change_Time = deal.StageChangeTime;
			objDeal.Active = deal.Active;
			objDeal.Deleted = deal.Deleted;
			objDeal.Status = deal.Status;
			objDeal.Probability = deal.Probability;
			objDeal.Next_Activity_Date = deal.NextActivityDate;
			objDeal.Next_Activity_Time = deal.NextActivityTime;
			objDeal.Next_Activity_Id = deal.NextActivityId;
			objDeal.Last_Activity_Id = deal.LastActivityId;
			objDeal.Last_Activity_Date = deal.LastActivityDate;
			objDeal.Lost_Reason = deal.LostReason;
			objDeal.Visible_To = deal.VisibleTo;
			objDeal.Close_Time = deal.CloseTime;
			objDeal.Pipeline_Id = deal.PipelineId;
			objDeal.Won_Time = deal.WonTime;
			objDeal.First_Won_Time = deal.FirstWonTime;
			objDeal.Lost_Time = deal.LostTime;
			objDeal.Products_Count = deal.ProductsCount;
			objDeal.Files_Count = deal.FilesCount;
			objDeal.Notes_Count = deal.NotesCount;
			objDeal.Followers_Count = deal.FollowersCount;
			objDeal.Email_Messages_Count = deal.EmailMessagesCount;
			objDeal.Activities_Count = deal.ActivitiesCount;
			objDeal.Done_Activities_Count = deal.DoneActivitiesCount;
			objDeal.Undone_Activities_Count = deal.UndoneActivitiesCount;
			objDeal.Reference_Activities_Count = deal.ReferenceActivitiesCount;
			objDeal.Participants_Count = deal.ParticipantsCount;
			objDeal.Expected_Close_Date = deal.ExpectedCloseDate;
			objDeal.Last_Incoming_Mail_Time = deal.LastIncomingMailTime;
			objDeal.Last_Outgoing_Mail_Time = deal.LastOutgoingMailTime;
			objDeal.Stage_Order_Nr = deal.StageOrderNr;
			objDeal.Person_Name = deal.PersonName;
			objDeal.Org_Name = deal.OrgName;
			objDeal.Next_Activity_Subject = deal.NextActivitySubject;
			objDeal.Next_Activity_Type = deal.NextActivityType;
			objDeal.Next_Activity_Duration = deal.NextActivityDuration;
			objDeal.Next_Activity_Note = deal.NextActivityNote;
			objDeal.Formatted_Value = deal.FormattedValue;
			objDeal.Weighted_Value = deal.WeightedValue;
			objDeal.Formatted_Weighted_Value = deal.FormattedWeightedValue;
			objDeal.Weighted_Value_Currency = deal.WeightedValueCurrency;
			objDeal.Rotten_Time = deal.RottenTime;
			objDeal.Owner_Name = deal.OwnerName;
			objDeal.CC_Email = deal.CcEmail;
			objDeal.Org_Hidden = deal.OrgHidden;
			objDeal.Person_Hidden = deal.PersonHidden;

			return objDeal;
		}

		#endregion
	}
}
