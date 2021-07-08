using Microsoft.EntityFrameworkCore;
using SOARIntegration.SOAR.Shared.Xero.Common.Model;
using SOARIntegration.Xero.Common.Model;
using Xero.Api.BankTransactions.Data;

namespace Xero.Api.BankTransactions.Repository
{
	public class Context : DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}
			
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new BankTransactionsMap(modelBuilder.Entity<BankTransaction>());

			modelBuilder.Entity<BankTransaction>().HasKey("Id");
			modelBuilder.Entity<BankTransactionLineItem>().HasKey(c => new { c.LineItemId });
		}

		public DbSet<BankTransaction> BankTransactions { get; set; }
		public DbSet<BankTransactionLineItem> BankTransactionLineItems { get; set; }
		public DbSet<BankTransactionLineItemTrackingCategory> BankTransactionLineItemTrackingCategories { get; set; }
		public DbSet<BankTransactionTrackingCategory> BankTransactionTrackingCategories { get; set; }
		public DbSet<BankTransactionTrackingOption> BankTransactionTrackingOptions { get; set; }
	}
}
