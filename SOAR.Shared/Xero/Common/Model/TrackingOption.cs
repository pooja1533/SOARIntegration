using SOARIntegration.Xero.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SOARIntegration.Xero.Common.Model
{
	[Table("tbl_SOAR_XERO_TrackingOption")]
	public class TrackingOption
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public Guid TrackingCategoryId { get; set; }
		public string OrgName { get; set; }
		public DateTime? Audit_Created { get; set; }
	}
}
