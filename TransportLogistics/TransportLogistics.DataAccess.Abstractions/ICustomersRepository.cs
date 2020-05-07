using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface ICustomersRepository : IBaseRepository<Customer>
    {
        IEnumerable<Customer> FindByLastName(string nameToFind);
        Customer FindByEmail(string emailToFind);
        Customer FindByPhoneNo(string phoneNo);
        Customer GetCustomerByGuid(Guid customerId);

    }
}
