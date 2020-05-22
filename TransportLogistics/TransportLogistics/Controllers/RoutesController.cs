using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.Model;
using TransportLogistics.ViewModels.Orders;
using TransportLogistics.ViewModels.Routes;


namespace TransportLogistics.Controllers
{
    public class RoutesController : Controller
    {

        private readonly ILogger<RouteService> logger;
        private readonly RouteService routeService;
        private readonly VehicleService vehicleService;
        private readonly OrderService orderService;
        public RoutesController(ILogger<RouteService> logger, RouteService routeservice, VehicleService vehicleService, OrderService orderService)
        {
            this.logger = logger;
            routeService = routeservice;
            this.vehicleService = vehicleService;
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

       

        private List<SelectListItem> GetVehicleList()
        {
            var vehicles = vehicleService.GetAll();
            List<SelectListItem> vehicleNames = new List<SelectListItem>();

            foreach (var vehicle in vehicles)
            {
                vehicleNames.Add(new SelectListItem(vehicle.Name, vehicle.Id.ToString()));  
            }
            return vehicleNames;
        }
        private List<SelectListItem> GetOrderList()
        {
            var orders = orderService.GetAllOrders();
            List<SelectListItem> orderNames = new List<SelectListItem>();

            foreach (var order in orders)
            {
               orderNames.Add(new SelectListItem(order.Recipient.ToString(), order.Id.ToString()));
            }
            return orderNames;
        }
      /*
        [HttpGet]
        private IActionResult AddOrder([FromRoute]string id, [FromRoute]string routeId)
        {
            try
            {

                var orderId = id;
                AddOrderViewModel newOrderViewModel = new AddOrderViewModel()
                {
                    OrderId = id,
                    OrderList = GetOrderList(),
                    RouteId = routeId
                };
                return PartialView("_AddOrderPartial", newOrderViewModel);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to load information for Order {@Exception}", e.Message);
                logger.LogDebug("Failed to load information for Order {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        private IActionResult AddOrder([FromForm]AddOrderViewModel orderData)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var order = orderService.GetById(orderData.OrderId);
                    var route = routeService.GetById(orderData.RouteId);
                    RouteEntry entry = new RouteEntry() { };
                    entry.SetOrder(order);
                    route.RouteEntries.Add(entry);
                }
                return PartialView("_AddOrderPartial", orderData);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to create a new Order {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new Order {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }
        */
        [HttpGet]
        public IActionResult NewRoute(string id)
        {
            try {
                var vehicleId = id;
                NewRouteViewModel newRouteViewModel = new NewRouteViewModel()
                {
                    VehicleId = vehicleId,
                    VehicleList = GetVehicleList()
                };

                return PartialView("_NewRoutePartial", newRouteViewModel);
            }
            
            catch (Exception e)
            {
                logger.LogError("Failed to load information for Route {@Exception}", e.Message);
                logger.LogDebug("Failed to load information for Route {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult NewRoute([FromForm]NewRouteViewModel routeData)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var vehicle = vehicleService.GetById(routeData.VehicleId);

                    routeService.CreateRoute(vehicle);


                    return PartialView("_NewRoutePartial", routeData);
                }

                return PartialView("_NewRoutePartial", routeData);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to create a new Route {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new Route {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        public IActionResult RoutesTable()
        {
            var routesView = new RouteViewModel()
            {
                Routes = routeService.GetAllRoutes()
            };
            return PartialView("_RoutesTablePartial", routesView);

        }


        [HttpGet]
        public IActionResult Remove([FromRoute]string Id)
        {

            RemoveRouteViewModel removeViewModel = new RemoveRouteViewModel()
            {
                Id = Id
            };

            return PartialView("_RemoveRoutePartial", removeViewModel);
        }

        [HttpPost]
        public IActionResult Remove(RemoveRouteViewModel removeData)
        {

            routeService.Remove(removeData.Id);

            return PartialView("_RemoveRoutePartial", removeData);
        }



    }
}