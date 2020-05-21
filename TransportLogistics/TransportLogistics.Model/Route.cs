using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Route:DataEntity
    {
        public ICollection<RouteEntry> RouteEntries { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public static Route Create()
        {
            var route = new Route()
            {
                Id = Guid.NewGuid()
            }; 
            return route;
        }

        public void SetRouteEntries(ICollection<RouteEntry> routeEntries)
        {
            RouteEntries = routeEntries;
        }
    } 
}
