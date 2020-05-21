using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TransportLogistics.Data.Abstractions;
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

            VehicleRepository = new EFVehicleRepository(context);
            CustomerRepository = new EFCustomerRepository(context);
            TrailerRepository = new EFTrailerRepository(context);
            EmployeeRepository = new EFEmployeeRepository(context);
            DriverRepository = new EFDriverRepository(context);
            OrderRepository = new EFOrderRepository(context);
        }

        public ICustomerRepository CustomerRepository { get ; private set; }
        public ITrailerRepository TrailerRepository { get ; private set ; }
        public IEmployeeRepository EmployeeRepository { get; private set; }
        public IVehicleRepository VehicleRepository { get; private set; }
        public IDriverRepository DriverRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IRouteRepository RouteRepository { get; private set; }
        
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
