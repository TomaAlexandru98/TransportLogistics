using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.Models.Customers
{
    public class CustomerListViewModel
    {
        public IEnumerable<CustomerViewModel> CustomerViews { get; set; }
    }
}
