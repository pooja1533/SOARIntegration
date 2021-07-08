using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.PipeDrive.Common.Model
{
	[Table("tbl_SOAR_PipeDrive_Deal")]
	public class Deal : BaseEntity
	{		
		public string Stage_Id { get; set; }
		public string Title { get; set; }
		public string Value { get; set; }
		public string Currency { get; set; }
		public DateTime? Add_Time { get; set; }
		public DateTime? Update_Time { get; set; }
		public DateTime? Stage_Change_Time { get; set; }
		public string Active { get; set; }
		public string Deleted { get; set; }
		public string Status { get; set; }
		public string Probability { get; set; }
		public DateTime? Next_Activity_Date { get; set; }
		public DateTime? Next_Activity_Time { get; set; }
		public string Next_Activity_Id { get; set; }
		public string Last_Activity_Id { get; set; }
		public DateTime? Last_Activity_Date { get; set; }
		public string Lost_Reason { get; set; }
		public int? Visible_To { get; set; }
		public DateTime? Close_Time { get; set; }
		public int? Pipeline_Id { get; set; }
		public DateTime? Won_Time { get; set; }
		public DateTime? First_Won_Time { get; set; }
		public DateTime? Lost_Time { get; set; }
		public int? Products_Count { get; set; }
		public int? Files_Count { get; set; }
		public int? Notes_Count { get; set; }
		public int? Followers_Count { get; set; }
		public int? Email_Messages_Count { get; set; }
		public int? Activities_Count { get; set; }
		public int? Done_Activities_Count { get; set; }
		public int? Undone_Activities_Count { get; set; }
		public int? Reference_Activities_Count { get; set; }
		public int? Participants_Count { get; set; }
		public DateTime? Expected_Close_Date { get; set; }
		public DateTime? Last_Incoming_Mail_Time { get; set; }
		public DateTime? Last_Outgoing_Mail_Time { get; set; }
		public int? Stage_Order_Nr { get; set; }
		public string Person_Name { get; set; }
		public string Org_Name { get; set; }
		public string Next_Activity_Subject { get; set; }
		public string Next_Activity_Type { get; set; }
		public string Next_Activity_Duration { get; set; }
		public string Next_Activity_Note { get; set; }
		public string Formatted_Value { get; set; }
		public string Weighted_Value { get; set; }
		public string Formatted_Weighted_Value { get; set; }
		public string Weighted_Value_Currency { get; set; }
		public DateTime? Rotten_Time { get; set; }
		public string Owner_Name { get; set; }
		public string CC_Email { get; set; }
		public string Org_Hidden { get; set; }
		public string Person_Hidden { get; set; }
	}
}
