using SOARIntegration.Xero.Api.Payments.Repository;
using System.Collections.Generic;
using System;
using SOAR.Shared.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Payments.Service
{
	public class PaymentsService : IPaymentsService
    {
		private IRepository<Payment> _repository;

		public PaymentsService(IRepository<Payment> repository)
		{
			this._repository = repository;
		}

        public void InsertPayments(List<Payment> payments)
        {
            int saveCounter = 0;
            for (var count = 0; count < payments.Count; count++)
            {
                try
                {
                    var org = _repository.Get(payments[count].Id);
                    if (org == null)
                    {
                        _repository.Insert(payments[count]);
                    }
                    else
                    {
                        payments[count].Id = org.Id;
                        _repository.Update(payments[count]);
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
            Console.WriteLine("Completed updating payments.");
        }

        public IEnumerable<Payment> GetAllPayments()
		{
			return _repository.GetAll();
		}
	}
}
