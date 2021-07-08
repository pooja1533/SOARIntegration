using System;
using Xero.Api;
using Xero.Api.Core;
using SOARIntegration.Xero.Common.Authenticators;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Infrastructure.Applications.Private;
using Xero.Api.Infrastructure.Authenticators;
using MemoryTokenStore = Xero.Common.TokenStores.MemoryTokenStore;


namespace SOARIntegration.Xero.Common.Helpers
{
    public class Application
    {
        public Application()
        {
        }

        public static IXeroCoreApi Initialise(string company)
        {
            var companyConfig  = new XeroApiSettings($"appsettings.org.{company}.json" );

            switch (companyConfig.AppType.ToLower())
            {
                case "private":
                    return PrivateApp(company, companyConfig);
                case "public":
                    throw new ApplicationException("AppType public not supported");
                case "partner":
                    throw new ApplicationException("AppType partner not supported");
                default: throw new ApplicationException("AppType must be private");
            }
        }

        private static IXeroCoreApi PrivateApp(string company, XeroApiSettings settings)
        {
            return new XeroCoreApi
                 (
                    settings.BaseUrl,
                    new PrivateAuthenticator(settings.SigningCertificatePath, settings.SigningCertificatePassword),
                    new Consumer(settings.ConsumerKey, settings.ConsumerSecret),
                    new ApiUser() { Identifier = Environment.MachineName }
                 )
            { UserAgent = company};
        }
    }
}
