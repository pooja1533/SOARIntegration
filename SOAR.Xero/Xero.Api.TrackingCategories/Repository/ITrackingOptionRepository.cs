using SOARIntegration.Xero.Common.Model;
using System;
using System.Collections.Generic;

namespace SOARIntegration.Xero.Api.TrackingCategories.Repository
{
	public interface ITrackingOptionRepository<T> where T : TrackingOption
	{
		IEnumerable<T> GetAll();
		T Get(Guid Id);
		T Get(string organisationId);
		void Insert(T entity);
		void Update(T entity);
		void Delete(T entity);
		void Remove(T entity);
		void SaveChanges();
	}
}
