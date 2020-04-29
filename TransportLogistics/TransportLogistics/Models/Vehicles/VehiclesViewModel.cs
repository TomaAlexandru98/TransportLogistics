using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.Models.Vehicles
{
    public class VehiclesViewModel
    {
        public string VehicleName { get; set; }
        public int NumberOfTrailers { get; set; }
        // aici sau la driver: currentlocation, status of departure
    }
}
