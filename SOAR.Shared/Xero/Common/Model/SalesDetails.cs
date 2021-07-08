using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_SalesDetails")]
	public class SalesDetails
	{
		public Guid ItemId { get; set; }
		public decimal? UnitPrice { get; set; }
		public string AccountCode { get; set; }
		public string TaxType { get; set; }
	}
}
