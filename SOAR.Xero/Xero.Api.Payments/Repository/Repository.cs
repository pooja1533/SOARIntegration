using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SOAR.Shared.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Payments.Repository {
    public class Repository<T> : IRepository<T> where T : Payment {
		private readonly Context context;
		private DbSet<T> entities;
		string errorMessage = string.Empty;

		public Repository (Context context) {
			this.context = context;
			entities = context.Set<T> ();
		}
		public IEnumerable<T> GetAll () {
			return entities.AsEnumerable ();
		}

		public T Get (Guid paymentId) {
            return entities.SingleOrDefault(s => s.Id == paymentId);
		}
		public void Insert (T entity) {
			if (entity == null) {
				throw new ArgumentNullException ("entity");
			}
            entities.Add (entity);
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
            }
            context.Entry(entity).State = EntityState.Modified;
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