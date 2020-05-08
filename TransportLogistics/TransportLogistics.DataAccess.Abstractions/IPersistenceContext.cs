using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TransportLogistics.Data.Abstractions;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IPersistenceContext
    {
        IVehiclesRepository VehiclesRepository { get; }
        TransactionScope BeginTransaction();
        void SaveChanges();
        ICustomersRepository CustomersRepository { get;  }
        ITrailerRepository TrailersRepository { get;  }
        IEmployeeRepiository EmployeeRepiository { get; }
    }
}
