using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOARIntegration.Xero.Api.TrackingCategories.Repository
{
	public class TrackingOptionRepository<T> : ITrackingOptionRepository<T> where T : TrackingOption
	{
		#region Private Fields
		private readonly Context context;
		private DbSet<T> entities;
		#endregion

		#region CTOR
		public TrackingOptionRepository(Context context)
		{
			this.context = context;
			entities = context.Set<T>();
		} 
		#endregion

		public IEnumerable<T> GetAll()
		{
			return entities.AsEnumerable();
		}

		public T Get(Guid id)
		{
			return entities.Where(m => m.Id == id).FirstOrDefault();
		}

		public void Insert(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			entity.Audit_Created = DateTime.UtcNow;
			entities.Add(entity);
		}

		public void Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			var local = context.Set<T>()
				.Local
				.FirstOrDefault(entry => entry.Id.Equals(entity.Id));

			if (local.Id != null)
			{
				context.Entry(local).State = EntityState.Detached;
			}
			context.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.Remove(entity);
			context.SaveChanges();
		}
		public void Remove(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.Remove(entity);
		}

		public void SaveChanges()
		{
			context.SaveChanges();
		}
		private DateTime GetDefaultDate(DateTime date)
		{
			return date == DateTime.MinValue ? DateTime.Now : date;
		}

		public T Get(string organisationId)
		{
			throw new NotImplementedException();
		}
	}
}
