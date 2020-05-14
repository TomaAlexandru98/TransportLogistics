using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        IEnumerable<Customer> FindByName(string nameToFind);
        Customer FindByEmail(string emailToFind);
        Customer FindByPhoneNo(string phoneNo);
        new Customer GetById(Guid customerId);
        new IEnumerable<Customer> GetAll();
        bool RemoveCustomerWithLocations(Guid customerId);
        void AddLocationToCustomer(Guid customerId, LocationAddress address);
        Customer UpdateCustomer(Guid customerId, string name, string phoneNo, string email);
    }
}
