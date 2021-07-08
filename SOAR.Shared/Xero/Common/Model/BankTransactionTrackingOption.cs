using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_BankTransactionTrackingOption")]
	public class BankTransactionTrackingOption
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public Guid TrackingCategoryId { get; set; }
		public string OrgName { get; set; }
		public DateTime? Audit_Created { get; set; }
	}
}
