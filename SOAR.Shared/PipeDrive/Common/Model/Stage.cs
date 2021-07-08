using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.PipeDrive.Common.Model
{
    [Table("tbl_SOAR_PipeDrive_Stage")]
    public class Stage : BaseEntity
    {
        public string Order_Nr { get; set; }
        public string Name { get; set; }
        public string Active_Flag { get; set; }
        public string Deal_Probability { get; set; }
        public string Pipeline_Id { get; set; }
        public string Rotten_Flag { get; set; }
        public string Rotten_Days { get; set; }
        public DateTime Add_Time { get; set; }
        public DateTime Update_Time { get; set; }
        public string Pipeline_Name { get; set; }
    }
}
