using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFDispatcherRepository:EFBaseRepository<Dispatcher> , IDispatcherRepository
    {
        public EFDispatcherRepository(TransportLogisticsDbContext context):base(context)
        {

        }

        public Dispatcher GetByUserId(string userId)
        {
           return dbContext.Dispatchers.Where(o => o.UserId == userId).FirstOrDefault();
        }
    }
}
