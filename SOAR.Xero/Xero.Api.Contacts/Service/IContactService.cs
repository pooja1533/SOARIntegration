using System.Collections.Generic;
using SOARIntegration.Xero.Common.Model;

namespace SOARIntegration.Xero.Api.Contacts.Service
{
	public interface IContactService
    {
        void InsertContacts(List<Contact> contacts);
        IEnumerable<Contact> GetAllContacts();
	}
}
