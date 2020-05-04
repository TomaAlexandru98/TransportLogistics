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
    }
}
