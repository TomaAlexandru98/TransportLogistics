using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Requests
{
    public class DepartureRequestsViewModel
    {
        public IEnumerable<DepartureRequest> DepartureRequests { get; set; }
        public bool AreRequestsActive { get; internal set; }
    }
}
