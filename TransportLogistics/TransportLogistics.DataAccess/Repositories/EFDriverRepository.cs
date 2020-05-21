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
        public Driver GetDriverWithRoute(Guid id)
        {
            return dbContext.Drivers.Include(o => o.CurrentRoute).ThenInclude(o=> o.RouteEntries).Include(o=> o.RoutesHistoric).
                Where(o=> o.Id == id).FirstOrDefault();
        }
        public ICollection<RouteEntry> GetRouteEntries(Guid id)
        {
            var driver = GetDriverWithRoute(id);
            ICollection<RouteEntry> routeEntries = new List<RouteEntry>();
            ICollection<RouteEntry> completeRouteEntries = new List<RouteEntry>();
            if (driver.CurrentRoute != null && driver.CurrentRoute.RouteEntries.Count > 0)
            {
                routeEntries = driver.CurrentRoute.RouteEntries;
               foreach (var routeEntry in routeEntries)
               {
                    var tempRouteEntry = dbContext.RouteEntries.Include(o => o.Order).Where(o => o.Id == routeEntry.Id).FirstOrDefault();
                    var order = dbContext.Orders.Include(o => o.PickUpAddress).Include(o => o.DeliveryAddress).
                        Where(o => o.Id == tempRouteEntry.Order.Id).FirstOrDefault();
                    tempRouteEntry.SetOrder(order);

                    completeRouteEntries.Add(tempRouteEntry);
               }
            }
            return completeRouteEntries;
        }

        public RoutesHistory GetRoutesHistory(Guid id)
        {
            var driver = GetById(id);
            var driverRoutes = dbContext.Drivers.Include(o => o.RoutesHistoric).ThenInclude(o => o.Routes)
                .Where(o=> o.Id == driver.Id).FirstOrDefault();
            ICollection<Route> routes = new List<Route>();
            if(driverRoutes.RoutesHistoric.Routes.Count > 0)
            {
                foreach(var route in driverRoutes.RoutesHistoric.Routes)
                {
                    var routeOrders = dbContext.Routes.Include(o => o.RouteEntries).Where(o => o.Id == route.Id).FirstOrDefault();
                    ICollection<RouteEntry> routeEntries = new List<RouteEntry>();
                    if(routeOrders.RouteEntries != null)
                    {
                        foreach(var routeEntry in routeOrders.RouteEntries)
                        {
                            var temp = dbContext.RouteEntries.Include(o=> o.Order).ThenInclude(o=> o.PickUpAddress).Include(o=>o.Order)
                                .ThenInclude(o=> o.DeliveryAddress).Where(o => o.Id == routeEntry.Id).FirstOrDefault();
                            routeEntries.Add(temp);
                        }
                        routeOrders.SetRouteEntries( routeEntries);
                        routes.Add(routeOrders);
                    }

                }
                driver.RoutesHistoric.SetRoutes(routes);
            }
            return driver.RoutesHistoric;
        }
       
    }
}
