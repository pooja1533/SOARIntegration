using System.Collections.Generic;
using SOARIntegration.Xero.Common.Model;
namespace SOARIntegration.Xero.Api.ExpenseClaims.Service
{
	public interface IExpenseClaimsService
    {
        void InsertExpenseClaims(List<ExpenseClaim> expenseClaims);

		IEnumerable<ExpenseClaim> GetAllExpenseClaims();
	}
}
