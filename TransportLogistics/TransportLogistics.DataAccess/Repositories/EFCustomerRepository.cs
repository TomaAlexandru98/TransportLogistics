using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFCustomerRepository : EFBaseRepository<Customer>, ICustomerRepository
    {
        public EFCustomerRepository(TransportLogisticsDbContext dbContext) : base(dbContext)
        { }

        public IEnumerable<Customer> FindByName(string nameToFind)
        {
            var customersList = dbContext.Customers
                                .Where(customer =>
                                            customer.Name
                                            .ToLower()
                                            .Contains(nameToFind.ToLower()));

            return customersList.AsEnumerable();
        }

        public Customer FindByEmail(string emailToFind)
        {
            var foundCustomer = dbContext.Customers
                            .Where(customer =>
                                        customer.ContactDetails.Email
                                        .Contains(emailToFind)).FirstOrDefault();

            return foundCustomer;
        }

        public Customer FindByPhoneNo(string phoneNo)
        {
            var foundCustomer = dbContext.Customers
                            .Where(customer =>
                                    customer.ContactDetails.PhoneNo
                                    .Contains(phoneNo)).FirstOrDefault();
           
            return foundCustomer;
        }

        public new Customer GetById(Guid customerId)
        {
            return dbContext.Customers.Include(c => c.ContactDetails)
                                        .Include(c => c.LocationAddresses)
                                        .Where(customer => customer.Id == customerId)
                                        .FirstOrDefault();
        }

        public void AddLocationToCustomer(Guid customerId, LocationAddress locationAddress)
        {
            var customer = GetById(customerId);
            dbContext.LocationAddresses.Add(locationAddress);
            customer.AddLocationAddress(locationAddress);
            dbContext.SaveChanges();
        }

        public bool RemoveCustomerWithLocations(Guid customerId)
        {
            var entityToRemove = GetById(customerId);
            
            if (entityToRemove != null)
            {
                if (entityToRemove.LocationAddresses.Count() > 0)
                {
                    foreach (LocationAddress location in entityToRemove.LocationAddresses)
                    {
                        dbContext.Remove(location);
                    }
                }

                dbContext.Remove(entityToRemove.ContactDetails);
                dbContext.Remove(entityToRemove);
                dbContext.SaveChanges();
                
                return true;
            }
            return false;
        }

        public Customer UpdateCustomer(Guid customerId, string name, string phoneNo, string email)
        {
            var customerToUpdate = GetById(customerId);

            var contact = customerToUpdate.UpdateContactDetails(phoneNo, email);
            customerToUpdate.UpdateCustomer(name, contact);

            dbContext.SaveChanges();

            return customerToUpdate;
        }
    }
}
