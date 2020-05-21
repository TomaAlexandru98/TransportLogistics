using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Route : DataEntity
    {
        public ICollection<RouteEntry> RouteEntries { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime FinishTime { get; private set; }
       
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
        public void SetStartTime()
        {
            StartTime = DateTime.UtcNow;
        }
        public void SetFinishTime()
        {
            FinishTime = DateTime.UtcNow;
           
        }
    } 
}
