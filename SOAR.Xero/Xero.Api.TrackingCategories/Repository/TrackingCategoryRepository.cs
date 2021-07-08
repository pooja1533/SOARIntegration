using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOARIntegration.Xero.Api.TrackingCategories.Repository
{
	public class TrackingCategoryRepository<T> : ITrackingCategoryRepository<T> where T : TrackingCategory
	{
		#region Private Fields
		private readonly Context context;
		private DbSet<T> entities;
		#endregion

		#region CTOR
		public TrackingCategoryRepository(Context context)
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
		//#region Private Fields
		//private readonly Context context;
		//private DbSet<T> entities;
		//#endregion

		//#region CTOR
		//public TrackingCategoryRepository(Context context)
		//{
		//	this.context = context;
		//	entities = context.Set<T>();
		//	EntityFrameworkManager.ContextFactory = context1 => this.context;
		//}
		//#endregion

		//#region GetRecordsOfCompanyByPaySlipDate
		//public IEnumerable<TrackingCategory> GetTrackingCategoriesByOrg(string orgName)
		//{
		//	return entities.Where(o => o.OrgName == orgName).ToList();
		//}
		//#endregion

		//#region BulkUpdate
		//public void BulkUpdate(List<T> entities)
		//{
		//	this.context.BulkUpdate(entities);
		//}
		//#endregion

		//#region BulkInsert
		//public void BulkInsert(List<T> entities)
		//{
		//	this.context.BulkInsert(entities);
		//}
		//#endregion

		//#region BulkDelete
		//public void BulkDelete(List<T> entities)
		//{
		//	this.context.BulkDelete(entities);
		//}
		//#endregion
	}
}
