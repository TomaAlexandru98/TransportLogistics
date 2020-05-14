using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFDriverRepository:EFBaseRepository<Driver>, IDriverRepository
    {
        public EFDriverRepository(TransportLogisticsDbContext dbContext) : base(dbContext)
        {
            
        }
        public Driver GetByUserId(string userId)
        {
            var driver = dbContext.Drivers.Where(o => o.UserId == userId).FirstOrDefault();
            return driver;
        }

        public ICollection<Order> GetOrders(Guid id)
        {
            var driver = GetById(id);
            return driver.Orders;
        }
    }
}
