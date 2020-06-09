using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Routes
{
    public class RouteEntriesViewModel
    {
        public ICollection<RouteEntry> RouteEntries { get; set; }
    }
}
