using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAR.Shared.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_Item")]
	public class Item
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public string InventoryAssetAccountCode { get; set; }
		public decimal? QuantityOnHand { get; set; }
		public bool? IsSold { get; set; }
		public bool? IsPurchased { get; set; }
		public string PurchaseDescription { get; set; }
		public bool IsTrackedAsInventory { get; set; }
		public decimal? TotalCostPool { get; set; }
		public string Name { get; set; }
        public string OrgName { get; set; }
    }
}
