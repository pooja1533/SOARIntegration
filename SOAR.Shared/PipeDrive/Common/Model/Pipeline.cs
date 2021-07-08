using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.PipeDrive.Common.Model
{
    [Table("tbl_SOAR_PipeDrive_Pipeline")]
	public class Pipeline : BaseEntity
	{
		public string Name { get; set; }
		public string URL_Title { get; set; }
		public int Order_Nr { get; set; }
		public string Active { get; set; }
		public string Deal_Probability { get; set; }
		public DateTime? Add_Time { get; set; }
		public DateTime? Update_Time { get; set; }
		public string Selected { get; set; }		
	}
}
