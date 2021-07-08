using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.PipeDrive.Common.Model
{
	[Table("tbl_SOAR_PipeDrive_Organization")]
	public class Organization : BaseEntity
	{
		public string Name { get; set; }
		public int Open_Deals_Count { get; set; }
		public int Related_Open_Deals_Count { get; set; }
		public int Closed_Deals_Count { get; set; }
		public int Related_Closed_Deals_Count { get; set; }
		public int Email_Messages_Count { get; set; }
		public int People_Count { get; set; }
		public int Activities_Count { get; set; }
		public int Done_Activities_Count { get; set; }
		public int UnDone_Activities_Count { get; set; }
		public int Reference_Activities_Count { get; set; }
		public int Files_Count { get; set; }
		public int Notes_Count { get; set; }
		public int Followers_Count { get; set; }
		public int Won_Deals_Count { get; set; }
		public int Related_Won_Deals_Count { get; set; }
		public int Lost_Deals_Count { get; set; }
		public int Related_Lost_Deals_Count { get; set; }
		public string Active_Flag { get; set; }
		public string Category_Id { get; set; }
		public string Picture_Id { get; set; }
		public string Country_Code { get; set; }
		public string First_Char { get; set; }
		public DateTime? Update_Time { get; set; }
		public DateTime? Add_Time { get; set; }
		public string Visible_To { get; set; }
		public DateTime? Next_Activity_Date { get; set; }
		public DateTime? Next_Activity_Time { get; set; }
		public string Next_Activity_Id { get; set; }
		public string Last_Activity_Id { get; set; }
		public DateTime? Last_Activity_Date { get; set; }
		public string Address { get; set; }
		public string Address_Subpremise { get; set; }
		public string Address_Street_Number { get; set; }
		public string Address_Route { get; set; }
		public string Address_Sublocality { get; set; }
		public string Address_Locality { get; set; }
		public string Address_Admin_Area_Level_1 { get; set; }
		public string Address_Admin_Area_Level_2 { get; set; }
		public string Address_Country { get; set; }
		public string Address_Postal_Code { get; set; }
		public string Address_Formatted_Address { get; set; }
		public string Owner_Name { get; set; }
		public string Owner_Id { get; set; }
		public string CC_Email { get; set; }
	}
}
