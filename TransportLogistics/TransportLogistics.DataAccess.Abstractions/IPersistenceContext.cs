using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TransportLogistics.Data.Abstractions;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IPersistenceContext
    {
        TransactionScope BeginTransaction();
        void SaveChanges();
        ICustomersRepository CustomersRepository { get;  }
        ITrailersRepository TrailersRepository { get;  }
        IEmployeeRepiository EmployeeRepiository { get; }
    }
}
