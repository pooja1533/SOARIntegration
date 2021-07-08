using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_InvoiceLineItemTrackingCategory")]
	public class InvoiceLineItemTrackingCategory
	{
		public int Id { get; set; }
		public Guid LineItemId { get; set; }
		public Guid TrackingCategoryId { get; set; }
		public string Option { get; set; }
	}
}
