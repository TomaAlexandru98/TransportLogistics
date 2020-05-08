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

        public CustomerService(ICustomerRepository customerRepository, IPersistenceContext persistenceContext)

        {
            this.persistenceContext = persistenceContext;
            this.customerRepository = customerRepository;
        }

        public Customer GetCustomerById(string customerId)
        {
            Guid.TryParse(customerId, out Guid guid);
            var customer = customerRepository?.GetCustomerByGuid(guid);

            if (customer == null)
            {
                throw new CustomerNotFoundException(guid);
            }

            return customer;
        }

        public Customer GetCustomerById(Guid customerId)
        {
            return customerRepository.GetCustomerByGuid(customerId);
        }

        public IEnumerable<LocationAddress> GetCustomerAddresses(string customerId)
        {

            var customer = GetCustomerById(customerId);

            return customer.LocationAddresses
                            .AsEnumerable();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return customerRepository.GetAll();
        }

        public Customer CreateNewCustomer(string name, string phoneNo, string email)
        {
            var customer = Customer.Create(name, phoneNo, email);
            customer = customerRepository.Add(customer);
            persistenceContext.SaveChanges();
            return customer;
        }

        public void AddLocationToCustomer(Guid customerId, LocationAddress locationAddress)
        {
            customerRepository.AddLocationToCustomer(customerId, locationAddress);
            persistenceContext.SaveChanges();
        }

        public bool RemoveCustomerById(string customerId)
        {
            var customer = GetCustomerById(customerId);

            if (customer == null)
                return false;

            customerRepository.RemoveCustomerWithLocations(customer.Id);
            persistenceContext.SaveChanges();

            return true;
        }

        public Customer UpdateCustomer(Guid customerId, string name, string phoneNo, string email)
        {
            var customerToUpdate = customerRepository.UpdateCustomer(customerId, name, phoneNo, email);

            return customerToUpdate;
        }
    }
}
