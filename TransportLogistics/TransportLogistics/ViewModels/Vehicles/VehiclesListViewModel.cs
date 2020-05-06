using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Vehicles
{
    public class VehiclesListViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}
