using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Organisations.Repository {
    public class Repository<T> : IRepository<T> where T : Organisation {
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

		public T Get (string organisationId) {
			return entities.Include(p => p.Addresses).Include(p => p.Phones).SingleOrDefault (s => s.OrganisationId == organisationId);
		}
		public void Insert (T entity) {
			if (entity == null) {
				throw new ArgumentNullException ("entity");
			}
            entity.Created = GetDefaultDate(entity.Created);
            entity.Modified = GetDefaultDate(entity.Modified);
            entities.Add (entity);
			context.SaveChanges ();
		}

		public void Update (T entity) {
			if (entity == null) {
				throw new ArgumentNullException ("entity");
			}
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (local.Id > 0)
            {
                context.Entry(local).State = EntityState.Detached;
                if(local.Addresses != null)
                foreach (var address in local.Addresses)
                    {
                        context.Entry(address).State = EntityState.Detached;
                    }
                if (local.Phones != null)
                    foreach (var phone in local.Phones)
                    {
                        context.Entry(phone).State = EntityState.Detached;
                    }

            }
            context.Entry(entity).State = EntityState.Modified;
            if (entity.Addresses != null)
                foreach (var address in entity.Addresses)
                {
                    var localaddress = local.Addresses
                    .FirstOrDefault(entry => address.OrganisationId.Equals(entity.Id));

                    if(localaddress == null)
                    {
                        context.Entry(address).State = EntityState.Added;
                    }
                    else 
                    {
                        context.Entry(address).State = EntityState.Modified;
                    }
                }
            if (entity.Phones != null)
                foreach (var phone in entity.Phones)
                {
                    var localPhone = local.Phones
                    .FirstOrDefault(entry => phone.OrganisationId.Equals(entity.Id));

                    if (localPhone == null)
                    {
                        context.Entry(phone).State = EntityState.Added;
                    }
                    else
                    {
                        context.Entry(phone).State = EntityState.Modified;
                    }

                }
            entity.Modified = GetDefaultDate(entity.Modified);

            context.SaveChanges ();
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