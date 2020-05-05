using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Driver: Employee
    {
        public ICollection<Vehicle> VehicleHistory { get; protected set; }
        //replace Vehicle with another model which beside a vehicle also contains a span of time when that car was used by driver
        //public ICollection<Tuple<LocationAddressData, LocationAddressData>> RoutesHistory { get; protected set; }
        public ICollection<Order> Orders { get; protected set; }
    }
}
