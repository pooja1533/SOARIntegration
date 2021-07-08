using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Users.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
	{
        private readonly Context context;
        private DbSet<T> entities;

        public Repository(Context context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public T Get(string entityId)
        {
            return entities.SingleOrDefault(s => s.EntityId == entityId);
        }
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
    }
}
