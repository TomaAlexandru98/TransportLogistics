using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;


namespace TransportLogistics.Model
{
    public class Customer : DataEntity    {
        public string FirstName { get; protected set; }
        public string Name { get; protected set; }
        public virtual Contact ContactDetails { get; protected set; }
        public virtual ICollection<LocationAddress> LocationAddresses { get; protected set; }

        protected Customer()
        { }

        public static Customer Create(string name)
        {
            var createdCustomer = new Customer()
            {
                Id = Guid.NewGuid(),
                Name = name,
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
