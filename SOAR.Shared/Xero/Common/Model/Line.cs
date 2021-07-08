using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_Line")]
	public class Line
	{
		public int AutoId { get; set; }
		public Guid ReferenceId { get; set; }
		public Guid AccountId { get; set; }
		public Guid Id { get; set; }
		public string AccountCode { get; set; }
		public string AccountType { get; set; }
		public string AccountName { get; set; }
		public decimal? NetAmount { get; set; }
		public decimal? GrossAmount { get; set; }
		public decimal? TaxAmount { get; set; }
		public string TaxType { get; set; }
		public string TaxName { get; set; }
		//public ItemTracking TrackingCategories { get; set; }
		//public ItemTracking Tracking { get; set; }
		public decimal? Amount { get; set; }
		public string Description { get; set; }
	}
}
