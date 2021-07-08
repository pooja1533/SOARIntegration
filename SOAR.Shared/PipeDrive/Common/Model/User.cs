using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.PipeDrive.Common.Model
{
    [Table("tbl_SOAR_PipeDrive_User")]
	public class User : BaseEntity
	{
		public string Name { get; set; }
		public string Default_Currency { get; set; }
		public string Locale { get; set; }
		public string Lang { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Activated { get; set; }
		public DateTime? Last_Login { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public string SignUp_Flow_Variation { get; set; }
		public string Has_Created_Company { get; set; }
		public string Is_Admin { get; set; }
		public string TimeZone_Name { get; set; }
		public string TimeZone_Offset { get; set; }
		public string Active_Flag { get; set; }
		public string Role_Id { get; set; }
		public string Icon_URL { get; set; }
		public string Is_You { get; set; }
	}
}
