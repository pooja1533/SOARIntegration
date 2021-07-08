using SOARIntegration.Xero.Api.BankTransfers.Repository;
using SOARIntegration.Xero.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOARIntegration.Xero.Api.BankTransfers.Service
{
	public class BankTransferService : IBankTransferService
	{
		private IRepository<BankTransfer> _repository;

		public BankTransferService(IRepository<BankTransfer> repository)
		{
			this._repository = repository;
		}

		public void InsertBankTRansfers(List<BankTransfer> bankTransfers)
		{
			int saveCounter = 0;
			for (var count = 0; count < bankTransfers.Count; count++)
			{
				try
				{
					var org = _repository.Get(bankTransfers[count].Id);
					if (org == null)
					{
						_repository.Insert(bankTransfers[count]);
					}
					else
					{
						bankTransfers[count].Id = org.Id;
						_repository.Update(bankTransfers[count]);
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
			Console.WriteLine("Completed updating banktransfers.");
		}

		public IEnumerable<BankTransfer> GetBankTransfers()
		{
			return _repository.GetAll();
		}
	}
}
