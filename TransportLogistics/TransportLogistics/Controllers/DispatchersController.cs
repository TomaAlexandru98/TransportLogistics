using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TransportLogistics.Controllers
{
    public class DispatchersController : Controller
    {
        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult OrdersTable()
        {
            return PartialView("_OrdersTablePartial");
        }
       
    }
}