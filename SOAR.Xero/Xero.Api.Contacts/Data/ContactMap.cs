using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.Xero.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xero.Api.Contacts.Data
{
   public class ContactMap
    {
        public ContactMap(EntityTypeBuilder<Contact> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.ContactID).IsRequired();
        }
    }
}
