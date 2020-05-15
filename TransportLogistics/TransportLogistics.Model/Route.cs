using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Route:DataEntity
    {
        public ICollection<Order> Orders { get; private set; }
        public static Route Create()
        {
            var route = new Route()
            {
                Id = Guid.NewGuid()
            }; 
        return route;
        }

    } 
}
