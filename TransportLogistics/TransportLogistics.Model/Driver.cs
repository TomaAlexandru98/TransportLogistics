using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Driver: Employee
    {
        public ICollection<Vehicle> VehicleHistory { get; protected set; }
        //public ICollection<Tuple<LocationAddressData, LocationAddressData>> RoutesHistory { get; protected set; }
        public ICollection<Order> Orders { get; protected set; }
    }
}
