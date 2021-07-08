﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SOARIntegration.Xero.Api.Contacts.Service;
using SOARIntegration.Xero.Api.Contacts.WebJob;
using System;
using System.IO;
using Xero.Api.Contacts.Repository;
using Xero.Api.Contacts.Service;
using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Common.Model;

namespace Xero.Api.Contacts
{
    class Program
    {
        static void Main(string[] args)
        {
            //Start-up
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

            //Configure services
            IConfigurationRoot configuration = builder.Build();
            var serviceProvider = new ServiceCollection()
                .AddLogging(b => b.AddConsole()
                .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning))
                .AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddSingleton<IContactService, ContactService>()
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IContactService>();
            var logger = serviceProvider.GetService<ILogger<ContactsModel>>();

            ContactsModel contactsModel = new ContactsModel(
                    logger,
                    service, 
                    serviceProvider.GetService<Context>(),
                    company,
                    configuration);

            contactsModel.Import();

        }
    }
}
