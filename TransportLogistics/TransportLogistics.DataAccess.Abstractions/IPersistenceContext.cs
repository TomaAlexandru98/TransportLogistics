using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IPersistenceContext
    {
        IVehicleRepository VehiclesRepository { get; }
        TransactionScope BeginTransaction();
        void SaveChanges();
    }
}
