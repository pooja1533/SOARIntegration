using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.PipeDrive.Common.Model
{
	[Table("tbl_SOAR_PipeDrive_Product")]
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public string Unit { get; set; }
		public int Tax { get; set; }
		public string Active_Flag { get; set; }
		public string Selectable { get; set; }
		public string First_Char { get; set; }
		public string Visible_To { get; set; }
		public string Owner_Id { get; set; }
		public int? Files_Count { get; set; }
		public int Followers_Count { get; set; }
		public DateTime Add_Time { get; set; }
		public DateTime Update_Time { get; set; }
	}
}
