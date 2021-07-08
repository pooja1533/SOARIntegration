using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_BankTransactionLineItemTrackingCategory")]
	public class BankTransactionLineItemTrackingCategory
	{
		public int Id { get; set; }
		public Guid LineItemId { get; set; }
		public Guid TrackingCategoryId { get; set; }
		public string Option { get; set; }
	}
}
