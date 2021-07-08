using SOARIntegration.SOAR.Shared.Xero.Common.Model;
using System.Collections.Generic;

namespace SOARIntegration.Xero.Api.BankTransactions.Service
{
	public interface IBankTransactionsService
    {
        void InsertBankTransactions(List<BankTransaction> purchaseOrders);

        IEnumerable<BankTransaction> GetAllBankTransactions();
	}
}
