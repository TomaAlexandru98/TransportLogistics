using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    class EFRouteRepository : EFBaseRepository<Route>, IRouteRepository
    {
        public EFRouteRepository(TransportLogisticsDbContext context) : base(context)
        { }

        public IEnumerable<RouteEntry> GetAllRouteEntries()
        {
            return dbContext.RouteEntries
                        .Include(r => r.Order)
                        .AsEnumerable();
        }


        public Route GetRouteById(Guid routeId)
        {
            var route = dbContext.Routes.Where(o => o.Id == routeId).FirstOrDefault();
            ICollection<RouteEntry> routeEntries = new List<RouteEntry>();
            if (route.RouteEntries != null)
            {
                foreach (var routeEntry in route.RouteEntries)
                {
                    var temp = dbContext.RouteEntries.Include(o => o.Order).ThenInclude(o => o.PickUpAddress).Include(o => o.Order)
                        .ThenInclude(o => o.DeliveryAddress).Where(o => o.Id == routeEntry.Id).FirstOrDefault();
                    routeEntries.Add(temp);
                }
                route.SetRouteEntries(routeEntries);
            }
            return route;
        }
    }
}
