using Microsoft.EntityFrameworkCore;
using SOARIntegration.PaySpace.Api.PaySlips.Repository;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System.Collections.Generic;
using System.Linq;

namespace PaySpace.Api.PaySlips.Repository
{
	public class PaySlipComponentRepository<T> : IPaySlipComponentRepository<T> where T : PaySlipComponent
	{
		#region Private Fields
		private readonly Context context;
		private DbSet<T> entities;
		#endregion

		#region CTOR
		public PaySlipComponentRepository(Context context)
		{
			this.context = context;
			entities = context.Set<T>();
		}
		#endregion

		#region GetRecordsOfCompanyByPaySlipDate
		public IEnumerable<PaySlipComponent> GetRecordsOfCompanyByPaySlipDate(string paySlipDate, string companyCode)
		{
			return entities.Where(o => o.PaySlipDate == paySlipDate && o.CompanyCode == companyCode).ToList();
		}
		#endregion		
	}
}
