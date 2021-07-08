using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_TrackingCategory")]
	public class TrackingCategory
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public string OrgName { get; set; }
		public DateTime? Audit_Created { get; set; }
	}
}
