using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TransportLogistics.DataAccess.Abstractions;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFPersistenceContext : IPersistenceContext
    {
        private readonly TransportLogisticsDbContext dbContext;
        private TransactionScope currentTransactionScope = null;
        public EFPersistenceContext(TransportLogisticsDbContext context)
        {
            this.dbContext = context;
            this.VehiclesRepository = new EFVehicleRepository(context);
        }

        public IVehiclesRepository VehiclesRepository 
        { 
            get;
            private set;
        }

        public TransactionScope BeginTransaction()
        {
            if (currentTransactionScope != null)
            {
                throw new TransactionException("A transaction is already in progress for this context");
            }
            currentTransactionScope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            return currentTransactionScope;
        }

        public void Dispose()
        {

            dbContext.Dispose();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
            if (currentTransactionScope != null)
            {
                currentTransactionScope.Complete();
            }

            currentTransactionScope = null;
        }
    }
}
