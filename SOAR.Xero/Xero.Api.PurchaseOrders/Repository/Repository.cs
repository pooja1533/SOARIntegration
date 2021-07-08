using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.PurchaseOrders.Repository {
	public class Repository<T> : IRepository<T> where T : PurchaseOrder {
		private readonly Context context;
		private DbSet<T> entities;
		string errorMessage = string.Empty;

		public Repository (Context context) {
			this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll () {
			return entities.AsEnumerable ();
		}

		public T Get (Guid id) {
            return entities.Include(m => m.Contact).Include(m => m.LineItems)/*.ThenInclude(m => m.Tracking)*/.SingleOrDefault (s => s.PurchaseOrderID == id);
        }

        //public void Insert(List<T> entityList)
        //{
        //    if (entityList == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }

        //    using (Context ctx = new Context(context.Database.GetDbConnection().ConnectionString))
        //    {
        //        foreach (var e in entityList)
        //        {
        //            e.Created = GetDefaultDate(entity.Created);
        //            e.Modified = GetDefaultDate(entity.Modified);
        //            entities.Add(entity);
        //        }
        //    }
        //}

        public void Insert (T entity) {
			if (entity == null) {
				throw new ArgumentNullException ("entity");
			}
            entity.Created = GetDefaultDate(entity.Created);
            entity.Modified = GetDefaultDate(entity.Modified);
            entities.Add(entity);
		}

		public void Update (T entity) {
			if (entity == null) {
				throw new ArgumentNullException ("entity");
			}
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (local.Id != null)
            {
                context.Entry(local).State = EntityState.Detached;
                foreach (var item in local.LineItems)
                {
                    context.Entry(item).State = EntityState.Detached;

                }
            }
            context.Entry(entity).State = EntityState.Modified;

            foreach (var item in entity.LineItems)
            {
                var localItem = local.LineItems
                .FirstOrDefault(entry => item.PurchaseOrderId.Equals(entity.PurchaseOrderID));

                if (localItem == null)
                {
                    context.Entry(item).State = EntityState.Added;
                }
                else
                {
                    context.Entry(item).State = EntityState.Modified;
                }

            }
            entity.Modified = GetDefaultDate(entity.Modified);
        }

		public void Delete (T entity) {
			if (entity == null) {
				throw new ArgumentNullException ("entity");
			}
			entities.Remove (entity);
			context.SaveChanges ();
		}
		public void Remove (T entity) {
			if (entity == null) {
				throw new ArgumentNullException ("entity");
			}
			entities.Remove (entity);
		}

		public void SaveChanges () {
			context.SaveChanges ();
		}
        private DateTime GetDefaultDate(DateTime date)
        {
            return date == DateTime.MinValue ? DateTime.Now : date;
        }
    }
}