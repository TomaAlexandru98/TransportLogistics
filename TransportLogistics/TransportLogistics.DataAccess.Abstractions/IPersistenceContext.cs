using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TransportLogistics.Data.Abstractions;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IPersistenceContext
    {
        ICustomerRepository CustomerRepository { get; }
        ITrailerRepository TrailerRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        IDriverRepository DriverRepository { get; }
        TransactionScope BeginTransaction();
        void SaveChanges();
    }
}
