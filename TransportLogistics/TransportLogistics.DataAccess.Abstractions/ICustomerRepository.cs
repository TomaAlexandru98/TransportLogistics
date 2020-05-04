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
    }
}
