using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace TransportLogistics.Data.Abstractions
{
    public interface IPersistenceContext : IDisposable
    {
        TransactionScope BeginTransaction();
        void SaveChanges();
    }
}
