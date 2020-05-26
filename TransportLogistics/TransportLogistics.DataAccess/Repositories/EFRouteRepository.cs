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
            var route = dbContext.Routes.Include(o=> o.RouteEntries).Where(o => o.Id == routeId).FirstOrDefault();
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

        public new IEnumerable<Route> GetAll()
        {
            return dbContext.Routes.Include(r => r.Vehicle).Include(e => e.RouteEntries).AsEnumerable();
        }

        public RouteEntry Add(RouteEntry entry,Guid routeId)
        {
            var route = dbContext.Routes.Include(r =>r.RouteEntries).Where(e => e.Id == routeId).FirstOrDefault();
            route.RouteEntries.Add(entry);
            
            dbContext.RouteEntries.Add(entry);
            dbContext.SaveChanges();
            return entry;
            
        }

        public RouteEntry GetEntry(Guid id)
        {
            //var Trailer = dbContext.Trailers.Where(trailer => trailer.Id == trailerId).FirstOrDefault();
            var entry = dbContext.RouteEntries.Where(e => e.Id == id).FirstOrDefault();
            return entry;
        }

        public void Remove(RouteEntry entry, Guid routeId)
        {
            
            var route = dbContext.Routes.Include(r => r.RouteEntries).Where(e => e.Id == routeId).FirstOrDefault();
            foreach(var dbentry in route.RouteEntries)
            {
               
                if(dbentry.Order.Id == entry.Order.Id)
                {
                    
                    route.RouteEntries.Remove(dbentry);
                    dbContext.RouteEntries.Remove(dbentry);
                    dbContext.SaveChanges();
                }
               
            }

        }

    }
}
