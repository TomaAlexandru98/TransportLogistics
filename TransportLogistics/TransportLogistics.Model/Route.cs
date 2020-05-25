using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Route : DataEntity
    {
        public ICollection<RouteEntry> RouteEntries { get;  set; }
        public Vehicle Vehicle { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime FinishTime { get; private set; }
        public static Route Create()
        {
            var route = new Route()
            {
                Id = Guid.NewGuid()
            };
            return route;
        }
       
        public static Route Create(Vehicle vehicle)
        {
            var route = new Route()
            {
                Id = Guid.NewGuid(),
                Vehicle = vehicle

            }; 
            return route;
        }

        public void SetRouteEntries(ICollection<RouteEntry> routeEntries)
        {
            RouteEntries = routeEntries;
        }

        public void SetRouteEntry(RouteEntry routeEntrie)
        {
            RouteEntries.Add(routeEntrie);
        }

        public void SetStartTime()
        {
            StartTime = DateTime.UtcNow;
        }
        public void SetFinishTime()
        {
            FinishTime = DateTime.UtcNow;
           
        }
        public void DeleteRouteEntries()
        {
            RouteEntries.Clear();
        }
    } 
}
