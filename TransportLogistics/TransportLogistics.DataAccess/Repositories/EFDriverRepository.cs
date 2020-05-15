using Microsoft.EntityFrameworkCore;
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
            var driver = dbContext.Drivers.
               Where(o => o.UserId == userId).FirstOrDefault();
            return driver;
        }
        private Driver GetDriverWithRoute(Guid id)
        {
            return dbContext.Drivers.Include(o => o.CurrentRoute).ThenInclude(o => o.Orders).Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Order> GetOrders(Guid id)
        {
            var driver = GetDriverWithRoute(id);

            ICollection<Order> ordersWithLoccations = new List<Order>();
            if (driver.CurrentRoute != null)
            {
                var orders = driver.CurrentRoute.Orders;
                foreach (var order in orders)
                {
                    var temp = dbContext.Orders.Where(o => o.Id == order.Id).Include(o => o.PickUpAddress).Include(o => o.DeliveryAddress).FirstOrDefault();
                    ordersWithLoccations.Add(temp);
                    //if (temp.Status == OrderStatus.PickedUp && temp.Type != OrderType.PickUp)
                    //{
                    //    if (temp.Status != OrderStatus.Delivered)
                    //    {

                    //  }
                    //}
                }
            }
            return ordersWithLoccations; 
           
        }
       
    }
}
