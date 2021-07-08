using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_LinkedTransaction")]
	public class LinkedTransaction
	{
		public Guid Id { get; set; }
		public Guid SourceTransactionID { get; set; }
		public Guid SourceLineItemID { get; set; }
		public Guid ContactID { get; set; }
		public Guid TargetTransactionID { get; set; }
		public Guid TargetLineItemID { get; set; }
		public string Status { get; set; }
		public string Type { get; set; }
	}
}
