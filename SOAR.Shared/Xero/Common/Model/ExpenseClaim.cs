using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Xero.Api.Common;
using Xero.Api.Core.Model.Status;

namespace SOARIntegration.Xero.Common.Model
{
    [Table("tbl_SOAR_Xero_ExpenseClaim")]
    public class ExpenseClaim : BaseEntity
    {
       
        public String ExpenseClaimID { get; set; }

        public String OrgName { get; set; }

        public String Status { get; set; }

        public double Total { get; set; }

        public double AmountDue { get; set; }

        public double AmountPaid { get; set; }

        public DateTime? PaymentDueDate { get; set; }

        public DateTime? ReportingDate { get; set; }

        public String UserId { get; set; }

        public DateTime? UpdatedDateUTC { get; set; }


        //[DataMember(EmitDefaultValue = false)]
       // public List<Receipt> Receipts { get; set; }

        //public List<Payment> Payments { get; set; }
    }
}
