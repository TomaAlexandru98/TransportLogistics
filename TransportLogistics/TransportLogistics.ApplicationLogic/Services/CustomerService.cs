using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
            var customer = customerRepository?.GetById(guid);

            if (customer == null)
            {
                throw new CustomerNotFoundException(guid);
            }

            return customer;
        }

        public Customer GetCustomerById(Guid customerId)
        {
            return customerRepository.GetById(customerId);
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
      

        public LocationAddress GetLocationAddress(string locationId)
        {
            Guid.TryParse(locationId, out Guid locationGuid);
            return customerRepository.GetLocationAddress(locationGuid);
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
            var customerToUpdate = GetCustomerById(customerId);

            var contact = customerToUpdate.UpdateContactDetails(phoneNo, email);
            customerToUpdate.UpdateCustomer(name, contact);
 
            persistenceContext.SaveChanges();

            return customerToUpdate;
        }

        public LocationAddress UpdateLocationAddress(Guid locationId, string country, string city, 
                                                    string street, int streetNumber, string postalCode)
        {
            var locationToUpdate = customerRepository.GetLocationAddress(locationId);

            locationToUpdate.Update(country, city, street, streetNumber, postalCode);
            persistenceContext.SaveChanges();

            return locationToUpdate;
        }

        public Customer GetCustomerByLocation(LocationAddress location)
        {
            var customer = customerRepository.GetCustomerByLocation(location);
            return customer;
        }

        public bool IsCustomer(string customerId)
        {
            Guid.TryParse(customerId, out Guid customerGuid);
            if (customerRepository.GetById(customerGuid) != null)
            {
                return true;
            }

            return false;
        }

    }
}
