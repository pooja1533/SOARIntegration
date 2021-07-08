using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOARIntegration.Xero.Common.Model {
	public class BaseEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Audit_Created")]
        public DateTime Created { get; set; }
        [Column("Audit_Modified")]
        public DateTime Modified { get; set; }
        [Column("Audit_IsDeleted")]
        public bool Deleted { get; set; }
	}
}