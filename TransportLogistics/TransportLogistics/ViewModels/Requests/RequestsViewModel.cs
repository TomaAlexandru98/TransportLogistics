using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Requests
{
    public class RequestsViewModel
    {
        public bool ShowMultipleRequestsModal { get; set; }
        public IEnumerable<Request> Requests { get; set; }
    }
}
