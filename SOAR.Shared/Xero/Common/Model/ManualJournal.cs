using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_ManualJournal")]
	public class ManualJournal
	{
		public Guid Id { get; set; }
		public DateTime? Date { get; set; }
		public string Status { get; set; }
		public string LineAmountTypes { get; set; }
		public string Url { get; set; }
		public bool? ShowOnCashBasisReports { get; set; }
		public string Narration { get; set; }
		public bool? HasAttachments { get; set; }
        public string OrgName { get; set; }
	}
}
