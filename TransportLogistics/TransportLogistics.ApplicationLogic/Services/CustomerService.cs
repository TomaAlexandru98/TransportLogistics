using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.ApplicationLogic.Exceptions;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
    public class CustomerService
    {
        private readonly IPersistenceContext persistenceContext;
        private readonly ICustomerRepository customerRepository;

        public CustomerService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
            customerRepository = persistenceContext.CustomerRepository;
        }

        public IEnumerable<LocationAddress> GetCustomerLocationAddresses(string customerId)
        {
            Guid.TryParse(customerId, out Guid guid);
            var customer = customerRepository?.GetById(guid);

            if (customer == null)
            {
                throw new CustomerNotFoundException(guid);
            }

            return customer.LocationAddresses
                            .AsEnumerable();
        }

    }
}
