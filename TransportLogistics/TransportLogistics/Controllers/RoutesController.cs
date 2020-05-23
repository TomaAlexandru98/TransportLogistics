﻿using System;
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
        private readonly OrderService orderService;
        private readonly VehicleService vehicleService;
        public RoutesController(ILogger<RouteService> logger, RouteService routeService, 
            OrderService orderService, VehicleService vehicleService)
        {
            this.logger = logger;
            this.routeService = routeService;
            this.orderService = orderService;
            this.vehicleService = vehicleService;
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

        private List<SelectListItem> GetOrdersList()
        {
            var orders = orderService.GetAllOrders();
            List<SelectListItem> orderNames = new List<SelectListItem>();

            foreach (var order in orders)
            {
                orderNames.Add(CreateOrderItem(order));
            }
            return orderNames;
        }

        private SelectListItem CreateOrderItem(Order order)
        {
            string dropdownText = $"{ order.PickUpAddress }, { order.DeliveryAddress }";
            SelectListItem selectLocation = new SelectListItem(dropdownText, order.Id.ToString());
            return selectLocation;
        }

        [HttpGet]
        public IActionResult NewRoute(string id)
        {
            try {
                   
                var vehicleId = id;
                
                NewRouteViewModel newRouteViewModel = new NewRouteViewModel()
                {
                    VehicleId = vehicleId,
                    OrderList = GetOrdersList(),
                    VehicleList = GetVehicleList()
                };

                return PartialView("_NewRoutePartial", newRouteViewModel);
            }
            
            catch (Exception e)
            {
                logger.LogError("Failed to load information for Order {@Exception}", e.Message);
                logger.LogDebug("Failed to load information for Order {@ExceptionMessage}", e);
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