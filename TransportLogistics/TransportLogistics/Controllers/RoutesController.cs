using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.ViewModels.Routes;


namespace TransportLogistics.Controllers
{
    public class RoutesController : Controller
    {

        private readonly ILogger<RouteService> logger;
        private readonly RouteService routeService;

        public RoutesController(RouteService routeservice, ILogger<RouteService> logger)
        {
            this.logger = logger;
            this.routeService = routeservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoutesTable()
        {
            /*
            var routesView = new RouteViewModel()
            {
                Routes = routeService.GetAllRoutes()
            }; , routesView*/
            return PartialView("_RoutesTablePartial");

        }
    }
}