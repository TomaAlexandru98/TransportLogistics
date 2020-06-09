using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Vehicles
{
    public class HistoryVehicleViewModel
    {
        public Vehicle Vehicle { get; set; }
        public IEnumerable<VehicleDriver> VehicleDriver { get; set; }
    }
}
