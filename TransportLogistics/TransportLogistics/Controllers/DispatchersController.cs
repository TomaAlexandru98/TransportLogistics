using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TransportLogistics.Controllers
{
    public class DispatchersController : Controller   //Delete and use other controllers sau exclusive pentru el
    {
        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult OrdersTable()
        {
            return PartialView("_OrdersTable");
        }
       
        public IActionResult Drivers()
        {
            return PartialView("_OrdersTablePartial");
        }
        public IActionResult Routes()
        {
            return View();
        }
    }
}