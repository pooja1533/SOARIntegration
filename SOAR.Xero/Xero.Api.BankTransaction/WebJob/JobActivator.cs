using System;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;

namespace SOARIntegration.Xero.Api.BankTransactions.WebJob {
    public class JobActivator : IJobActivator {
        private readonly IServiceProvider services;

        public JobActivator (IServiceProvider services) {
            this.services = services;
        }

        public T CreateInstance<T> () {
            return services.GetService<T> ();
        }
    }
}
