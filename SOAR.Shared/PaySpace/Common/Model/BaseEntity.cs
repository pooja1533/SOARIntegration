using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.SOAR.Shared.PaySpace.Common.Model
{
	public class BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string EntityId { get; set; }
		[Column("Audit_Created")]
		public DateTime Audit_Created { get; set; }
		[Column("Audit_Modified")]
		public DateTime Audit_Modified { get; set; }
		[Column("Audit_IsDeleted")]
		public bool Audit_Deleted { get; set; }
	}
}
