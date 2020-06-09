using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Customers
{
    public class CustomerListViewModel
    {
        public IEnumerable<Customer> CustomerViews { get; set; }
    }
}
