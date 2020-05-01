using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Data
{
    public class DriverData : EmployeeData
    {
        // Route ???
        public ICollection<VehicleData> VehicleHistory { get; protected set; }
        //public ICollection<Tuple<LocationAddressData, LocationAddressData>> RoutesHistory { get; protected set; }
        public ICollection<OrderData> Orders { get; protected set; }
    }
}
