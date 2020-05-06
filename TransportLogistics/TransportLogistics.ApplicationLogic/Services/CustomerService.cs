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
        //private readonly IPersistenceContext persistenceContext;
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            //this.persistenceContext = persistenceContext;
            this.customerRepository = customerRepository;
        }

        public IEnumerable<LocationAddress> GetCustomerAddresses(string customerId)
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

        public Customer GetCustomerByGuid(Guid customerId)
        {
            return customerRepository.GetCustomerByGuid(customerId);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return customerRepository.GetAll();
        }

    }
}
