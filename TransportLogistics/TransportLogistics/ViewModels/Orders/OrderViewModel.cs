using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Orders
{
    public class OrderViewModel
    {
        public IEnumerable<Order> Orders {get; set;}
    }
}
