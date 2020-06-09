using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Requests
{
    public class ConnectRequestsViewModel
    {
        public bool ShowMultipleRequestsModal { get; set; }
        public bool AreRequestsActive { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Request> Requests { get; set; }
    }
}
