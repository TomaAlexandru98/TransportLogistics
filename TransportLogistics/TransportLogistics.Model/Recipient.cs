using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Recipient : DataEntity
    {
        public string Name { get; protected set; }
        public virtual Contact ContactDetails { get; protected set; }

        protected Recipient()
        { }

        public static Recipient Create(string name, string phoneNo, string email)
        {
            var createdRecipient = new Recipient()
            {
                Id = Guid.NewGuid(),
                Name = name,
                ContactDetails = Contact.Create(phoneNo, email)
            };

            return createdRecipient;
        }
    }
}
