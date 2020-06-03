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
                if(vehicle.Status == VehicleStatus.Free)
                {
                    vehicleNames.Add(new SelectListItem(vehicle.Name, vehicle.Id.ToString()));
                }
            }
            return vehicleNames;
        }
        private List<SelectListItem> GetOrderList()
        {
            var orders = orderService.GetAllOrders();
            List<SelectListItem> orderNames = new List<SelectListItem>();

            foreach (var order in orders)
            {
                var text = order.DeliveryAddress.PostalCode + " " + order.DeliveryAddress.City;
                orderNames.Add(new SelectListItem(text, order.Id.ToString()));
            }
            return orderNames;
        }

        private List<SelectListItem> GetOrderListFromRoute(string id)
        {
            var route = routeService.GetById(id);
            
            var routes = route.RouteEntries;
           // var orders = orderService.GetAllOrders();
            List<SelectListItem> orderNames = new List<SelectListItem>();

            foreach (var order in routes)
            {
                orderNames.Add(new SelectListItem(order.Order.DeliveryAddress.PostalCode, order.Id.ToString()));
            }
            return orderNames;
        }

        [HttpGet]
        public IActionResult AddOrder(string RouteId)
        {
            try
            {

                //var orderId = Id;
                AddOrderViewModel newOrderViewModel = new AddOrderViewModel()
                {
                    //OrderId = Id,
                    OrderList = GetOrderList(),
                    RouteId = RouteId,
                    OrderType = GetOrderType()
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

        private List<SelectListItem> GetOrderType()
        {
            
            List<SelectListItem> orderNames = new List<SelectListItem>();
            orderNames.Add(new SelectListItem("Both", OrderType.Both.ToString()));
            orderNames.Add(new SelectListItem("Delivery", OrderType.Delivery.ToString()));
            orderNames.Add(new SelectListItem("PickUp", OrderType.PickUp.ToString()));
            return orderNames;
        }

        [HttpPost]
        public IActionResult AddOrder([FromForm]AddOrderViewModel orderData)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var order = orderService.GetById(orderData.OrderId);
                    var route = routeService.GetById(orderData.RouteId);
                    RouteEntry entry = new RouteEntry() {Id = Guid.NewGuid() };
                    
                    entry.SetOrder(order);
                    entry.SetType(orderData.type);
                    routeService.AddEntry(route.Id.ToString(), entry);


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

        [HttpGet]
        public IActionResult OrderList(string id)
        {
            string[] splitId = id.Split(';');
            var orderId = splitId[0];
            string RouteId = splitId[1];
            
            AddOrderViewModel model = new AddOrderViewModel()
            {
                OrderId = orderId,
                OrderList = GetOrderList(),
               RouteId = RouteId

            };
            return PartialView("_AddOrderPartial", model);

        }

        [HttpPost]
        public IActionResult OrderList([FromForm]AddOrderViewModel data)
        {
            return AddOrder(data.RouteId);


        }

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
            
            routeService.RemoveRoute(removeData.Id);

            return PartialView("_RemoveRoutePartial", removeData);
        }

        [HttpGet]
        public IActionResult RemoveOrderList([FromRoute]string id)
        {
            string[] splitId = id.Split(';');
            var orderId = splitId[0];
            string RouteId = splitId[1];

            DeleteOrderViewModel model = new DeleteOrderViewModel()
            {
                //orderId = orderId,

                OrderList = GetOrderListFromRoute(RouteId),
                routeId = RouteId

            };
            return PartialView("_RemoveOrderPartial", model);
        }

        [HttpPost]
        public IActionResult RemoveOrderList([FromForm]DeleteOrderViewModel data)
        {
            return RemoveOrder(data.routeId);


        }

        [HttpGet]
        public IActionResult RemoveOrder(string RouteId)
        {
            try
            {

                //var orderId = Id;
                DeleteOrderViewModel newOrderViewModel = new DeleteOrderViewModel()
                {
                    //OrderId = Id,
                    OrderList = GetOrderList(),
                    routeId = RouteId
                };
                return PartialView("_RemoveOrderPartial", newOrderViewModel);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to load information for Order {@Exception}", e.Message);
                logger.LogDebug("Failed to load information for Order {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public IActionResult RemoveOrder([FromForm]DeleteOrderViewModel orderData)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var routeEntry = routeService.GetEntryById(orderData.orderId);
                    var route = routeService.GetById(orderData.routeId);
                 
                    routeService.RemoveEntry(route.Id.ToString(), routeEntry);


                }
                return PartialView("_RemoveOrderPartial", orderData);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to create a new Order {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new Order {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult ChangeVehicle([FromRoute]string id)
        {

            string RouteId = id;
            try
            {

                ChangeVehicleViewModel newRouteViewModel = new ChangeVehicleViewModel()
                {
                    RouteId = RouteId,
                    //VehicleId = vehicleId,
                    VehicleList = GetVehicleList()
                };

                return PartialView("_ChangeVehiclePartial", newRouteViewModel);
            }

            catch (Exception e)
            {
                logger.LogError("Failed to load information for Route {@Exception}", e.Message);
                logger.LogDebug("Failed to load information for Route {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult ChangeVehicle([FromForm]ChangeVehicleViewModel data)
        {
            var route = routeService.GetById(data.RouteId);
            var vehicle = vehicleService.GetById(data.VehicleId);
            routeService.ChangeVehicle(route,vehicle);
            
            return PartialView("_ChangeVehiclePartial", data);
        }


        [HttpGet]
        public IActionResult Map([FromRoute]string id)
        {
            var route = routeService.GetById(id);
            var entries = new RouteEntriesViewModel();
            entries.RouteEntries = route.RouteEntries;

            return View(entries);
        }

        [HttpGet]
        public IActionResult ShowEntry([FromRoute] string id)
        {

            var routeEntry = routeService.GetEntryById(id);

            var routeEntryViewModel = new RouteEntryViewModel()
            {
                RouteEntry = routeEntry
            };

            return PartialView("_RouteEntryPartial", routeEntryViewModel);
        }

        public IActionResult RouteMap()
        {
            return View();

        }
    }
}