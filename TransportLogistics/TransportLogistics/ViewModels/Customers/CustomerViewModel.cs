using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.Models.Customers
{
    public class CustomerViewModel
    {
        public string Id { get; set; }
        public string Name {get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public IEnumerable<LocationViewModel> LocationViews { get; set; }
    }
}
