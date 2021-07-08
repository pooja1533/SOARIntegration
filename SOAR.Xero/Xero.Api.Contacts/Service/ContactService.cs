using SOARIntegration.Xero.Api.Contacts.Service;
using SOARIntegration.Xero.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xero.Api.Contacts.Repository;

namespace Xero.Api.Contacts.Service
{
    public class ContactService: IContactService
    {
        private readonly IRepository<Contact> repository;

        public ContactService(IRepository<Contact> rep)
        {
            this.repository = rep;
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return repository.GetAll();
        }

        public void InsertContacts(List<Contact> contacts)
        {
            int saveCounter = 0;
            for (var count = 0; count < contacts.Count; count++)
            {
                try
                {
                    var contact = repository.Get(Guid.Parse(contacts[count].ContactID));
                    if (contact == null)
                    {
                        repository.Insert(contacts[count]);
                    }
                    else
                    {
                        contacts[count].Id = contact.Id;
                        contacts[count].Created = contact.Created;
                        
                        repository.Update(contacts[count]);
                    }
                    saveCounter++;

                    if (saveCounter % 500 == 0)
                    {
                        repository.SaveChanges();
                        Console.Write(".");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            repository.SaveChanges();
            Console.WriteLine($"Completed updating {contacts.Count} contacts.");
        }
    }
}
