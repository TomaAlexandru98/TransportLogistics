using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Vehicles
{
    public class RouteDetailsViewModel
    {
        public string VehicleId { get; set; }
        public IEnumerable<RouteEntry> RouteEntries { get; set; }
    }
}
