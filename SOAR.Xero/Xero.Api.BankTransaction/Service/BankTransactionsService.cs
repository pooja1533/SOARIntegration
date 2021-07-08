using SOARIntegration.SOAR.Shared.Xero.Common.Model;
using SOARIntegration.Xero.Api.BankTransactions.Repository;
using System;
using System.Collections.Generic;

namespace SOARIntegration.Xero.Api.BankTransactions.Service
{
	public class BankTransactionsService : IBankTransactionsService
    {
        private readonly IRepository<BankTransaction> _repository;

        public BankTransactionsService(IRepository<BankTransaction> repository)
		{
			this._repository = repository;
		}

        public void InsertBankTransactions(List<BankTransaction> bankTransactions)
        {
            int saveCounter = 0;
            for (var count = 0; count < bankTransactions.Count; count++)
            {
                try
                {
                    var bt = _repository.Get(bankTransactions[count].Id.ToString());
                    if (bt == null)
                    {
                        _repository.Insert(bankTransactions[count]);
                    }
                    else
                    {
                        bankTransactions[count].Id = bt.Id;
                        bankTransactions[count].Audit_Created = bt.Audit_Created;
                        _repository.Update(bankTransactions[count]);
                    }
                    saveCounter++;

                    if (saveCounter % 500 == 0)
                    {
                        _repository.SaveChanges();
                        Console.Write(".");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            _repository.SaveChanges();
            Console.WriteLine($"Completed updating {bankTransactions.Count} bank transactions.");
        }

        public IEnumerable<BankTransaction> GetAllBankTransactions()
		{
			return _repository.GetAll();
		}
	}
}
