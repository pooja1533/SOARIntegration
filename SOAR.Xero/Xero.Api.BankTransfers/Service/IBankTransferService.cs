using SOARIntegration.Xero.Common.Model;
using System.Collections.Generic;

namespace SOARIntegration.Xero.Api.BankTransfers.Service
{
	public interface IBankTransferService
	{
		void InsertBankTRansfers(List<BankTransfer> bankTransfers);

		IEnumerable<BankTransfer> GetBankTransfers();
	}
}
