using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;


namespace TransportLogistics.Model
{
    public class Customer : DataEntity    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public Contact ContactDetails { get; protected set; }
        public virtual ICollection<LocationAddress> LocationAddresses { get; protected set; }

        protected Customer()
        { }

        public static Customer Create(string firstName, string lastName)
        {
            var createdCustomer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                ContactDetails = new Contact(),
                LocationAddresses = new List<LocationAddress>()
            };

            return createdCustomer;
        }

        public void UpdateContactDetails(string phoneNo, string email)
        {
            ContactDetails.Update(phoneNo, email);
        }
    }

}
