using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_Journal")]
	public class Journal
	{
		public Guid Id { get; set; }
		public DateTime? Date { get; set; }
		public int Number { get; set; }
		public DateTime? CreatedDateUtc { get; set; }
		public string Reference { get; set; }
		public string SourceId { get; set; }
		public string SourceType { get; set; }
        public string OrgName { get; set; }
    }
}
