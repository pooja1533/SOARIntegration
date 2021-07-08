using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Api.BankTransfers.Data;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.BankTransfers.Repository
{
	public class Context : DbContext
	{
		#region CTOR
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
		#endregion

		#region OnModelCreating
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new BankTransferMap(modelBuilder.Entity<BankTransfer>());
		} 
		#endregion
	}
}
