#region NameSpace
using Microsoft.EntityFrameworkCore;
using SOARIntegration.SOAR.Shared.PaySpace.Common.Model;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace SOARIntegration.PaySpace.Api.PaySlips.Repository
{
	public class PaySlipLeaveRepository<T> : IPaySlipLeaveRepository<T> where T : PaySlipLeave
	{
		#region Private Fields
		private readonly Context context;
		private DbSet<T> entities;
		#endregion

		#region CTOR
		public PaySlipLeaveRepository(Context context)
		{
			this.context = context;
			entities = context.Set<T>();
		}
		#endregion

		#region GetRecordsOfCompanyByPaySlipDate
		public IEnumerable<PaySlipLeave> GetRecordsOfCompanyByPaySlipDate(string paySlipDate, string companyCode)
		{
			return entities.Where(o => o.PaySlipDate == paySlipDate && o.CompanyCode == companyCode).ToList();
		}
		#endregion		
	}
}
