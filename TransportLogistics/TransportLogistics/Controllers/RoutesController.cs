using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TransportLogistics.Controllers
{
    public class RoutesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoutesTable()
        {
            return PartialView("_RoutesTablePartial");
        }
    }
}