using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Dispatchers
{
    public class DriversListViewModel
    {
        public IEnumerable<Driver> Drivers { get; set; }

        public IEnumerable<Route> Routes{ get; set; }

    }
}
