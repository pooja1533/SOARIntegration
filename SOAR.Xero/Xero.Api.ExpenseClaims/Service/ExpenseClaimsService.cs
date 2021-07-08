using SOARIntegration.Xero.Common.Model;
using SOARIntegration.Xero.Api.ExpenseClaims.Repository;
using System.Collections.Generic;
using System;

namespace SOARIntegration.Xero.Api.ExpenseClaims.Service
{
	public class ExpenseClaimsService : IExpenseClaimsService
    {
		private IRepository<ExpenseClaim> _repository;

		public ExpenseClaimsService(IRepository<ExpenseClaim> repository)
		{
			this._repository = repository;
		}

        public void InsertExpenseClaims(List<ExpenseClaim> expenseClaims)
        {
            for (var count = 0; count < expenseClaims.Count; count++)
            {
                try
                {
                    var claim = _repository.Get(expenseClaims[count].ExpenseClaimID);
                    if (claim == null)
                    {
                        _repository.Insert(expenseClaims[count]);
                    }
                    else
                    {
                        expenseClaims[count].Id = claim.Id;
                        expenseClaims[count].Created = claim.Created;
                        _repository.Update(expenseClaims[count]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<ExpenseClaim> GetAllExpenseClaims()
		{
			return _repository.GetAll();
		}
	}
}
