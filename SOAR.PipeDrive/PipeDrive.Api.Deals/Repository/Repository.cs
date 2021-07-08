#region NameSpace
using Microsoft.EntityFrameworkCore;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;
using System;
using System.Linq;
#endregion

namespace SOARIntegration.PipeDrive.Api.Deals.Repository
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
    {
		#region Private Fields
		private readonly Context context;
		private DbSet<T> entities;
		#endregion

		#region CTOR
		public Repository(Context context)
		{
			this.context = context;
			entities = context.Set<T>();
		}
		#endregion

		#region Get
		public T Get(string entityId)
		{
			return entities.SingleOrDefault(s => s.EntityId == entityId);
		}
		#endregion

		#region Insert
		public void Insert(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			entity.Audit_Created = DateTime.UtcNow;
			entity.Audit_Modified = DateTime.UtcNow;
			entities.Add(entity);
			context.SaveChanges();
		}
		#endregion

		#region Update
		public void Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			var local = context.Set<T>()
			   .Local
			   .FirstOrDefault(entry => entry.EntityId.Equals(entity.EntityId));

			if (local.Id > 0)
			{
				context.Entry(local).State = EntityState.Detached;
			}
			context.Entry(entity).State = EntityState.Modified;
			entity.Audit_Modified = DateTime.UtcNow;

			context.SaveChanges();
		} 
		#endregion
	}
}
