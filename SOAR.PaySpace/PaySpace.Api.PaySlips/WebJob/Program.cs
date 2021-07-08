#region NameSpace
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaySpace.Api.PaySlips;
using PaySpace.Api.PaySlips.Repository;
using SoapHttpClient;
using SoapHttpClient.Enums;
using SOARIntegration.PaySpace.Api.PaySlips.Repository;
using SOARIntegration.PaySpace.Api.PaySlips.Service;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace SOARIntegration.PaySpace.Api.PaySlips.WebJob
{
	public class Program
	{
		static void Main()
		{
			var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
			//var connectionString = "Data Source=DESKTOP-9FOTS8I;Initial Catalog=Marvel_Prod_bck;Persist Security Info=True;User ID=sa;Password=sasa;MultipleActiveResultSets=True";
			//var connectionString = "Data Source=soar-db-preprod.database.windows.net;Initial Catalog=SOAR_DB_PREPROD;Persist Security Info=True;User ID=a1l-soar-preprod-admin;Password=0XZ-u8@nH8?dAq5T;MultipleActiveResultSets=True";
			//var connectionString = "Data Source=a1l-sql.database.windows.net;Initial Catalog=SOAR_DB;Persist Security Info=True;User ID=a1l-soar-admin;Password=0XZ-u8@nH8?dAq5T;MultipleActiveResultSets=True";
			var serviceProvider = new ServiceCollection()
				.AddLogging()
				.AddDbContext<Context>(options => options.UseSqlServer(connectionString))
				.AddScoped(typeof(IPaySlipLeaveRepository<>), typeof(PaySlipLeaveRepository<>))
				.AddScoped(typeof(IPaySlipComponentRepository<>), typeof(PaySlipComponentRepository<>))
				.AddSingleton<IPaySlipService, PaySlipService>()
				.BuildServiceProvider();

			var payslipService = serviceProvider.GetService<IPaySlipService>();

			var importService = new PaySlipServiceNew(
				   serviceProvider.GetService<ILogger<PaySlipServiceNew>>(),
				   serviceProvider.GetService<Context>());

			importService.Import();
		}
	}

	public partial class PaySlipsData
	{
		#region Private Fields
		private readonly IPaySlipService _payslipService;
		#endregion

		#region CTOR
		public PaySlipsData(IPaySlipService paySlipService)
		{
			this._payslipService = paySlipService;
		}
		#endregion
	}
}
