using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Common.Model;
using SOARIntegration.Xero.Api.PurchaseOrders.Data;
using SOARIntegration.SOAR.Shared.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.PurchaseOrders.Repository
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public Context(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new PurchaseOrdersMap(modelBuilder.Entity<PurchaseOrder>());

            modelBuilder.Entity<PurchaseOrderLineItem>().HasKey(c => new { c.LineItemId });
        }

		public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
		public DbSet<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
		public DbSet<PurchaseOrderLineItemTrackingCategory> PurchaseOrderLineItemTrackingCategories { get; set; }
		public DbSet<PurchaseOrderTrackingCategory> PurchaseOrderTrackingCategories { get; set; }
		public DbSet<PurchaseOrderTrackingOption> PurchaseOrderTrackingOptions { get; set; }
	}
}
