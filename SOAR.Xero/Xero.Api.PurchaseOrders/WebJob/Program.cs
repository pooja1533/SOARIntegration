using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SOARIntegration.Xero.Api.PurchaseOrders.Repository;
using SOARIntegration.Xero.Api.PurchaseOrders.Service;
using System;
using System.IO;
using System.Linq;

namespace SOARIntegration.Xero.Api.PurchaseOrders.WebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var company = "";

            if (args.Length == 0)
            {
                throw new Exception("Please enter a company indicator.");
            }
            else
            {
                company = args[0];
            }

            var sharedFolder = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "SOAR.Shared");
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.org.{company}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            var serviceProvider = new ServiceCollection()
                .AddLogging(b => b.AddConsole()
                .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning))
                .AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddSingleton<IPurchaseOrdersService, PurchaseOrdersService>()
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IPurchaseOrdersService>();
            var logger = serviceProvider.GetService<ILogger<PurchaseOrdersModel>>();

            PurchaseOrdersModel model = new PurchaseOrdersModel(
                    logger, 
                    service,
                    serviceProvider.GetService<Context>(),
                    company,
                    configuration);

            model.ProcessData();
        }



    }
}
