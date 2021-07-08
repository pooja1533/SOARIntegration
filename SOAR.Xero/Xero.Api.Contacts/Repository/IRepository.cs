using SOARIntegration.Xero.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xero.Api.Contacts.Repository
{
    public interface IRepository<T> where T : Contact
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
