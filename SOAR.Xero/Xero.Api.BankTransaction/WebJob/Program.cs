using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SOARIntegration.Xero.Api.BankTransactions.Repository;
using SOARIntegration.Xero.Api.BankTransactions.Service;
using System;
using System.IO;
using System.Linq;
using Xero.Api.BankTransactions.Repository;

namespace SOARIntegration.Xero.Api.BankTransactions.WebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var company = "";
			var syncMode = "-d";

			if (args.Length == 1)
			{
				company = args[0];
			}
			else if (args.Length == 2)
			{
				company = args[0];
				syncMode = args[1];
			}
			else
			{
				throw new Exception("Please specify a company indicator and (optionally) a sync mode (-f for a full sync, delta by default): Xero.Api.Accounts.exe <CompanyIndicator> <SyncMode> ");
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
                .AddSingleton<IBankTransactionsService, BankTransactionsService>()
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IBankTransactionsService>();
            var logger = serviceProvider.GetService<ILogger<BankTransactionsModel>>();

            BankTransactionsModel model = new BankTransactionsModel(
                    logger, 
                    serviceProvider.GetService<Context>(),
                    company,
                    configuration,syncMode);

            model.ProcessData();
        }
    }
}
