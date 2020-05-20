using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.ApplicationLogic.Sevices;
using TransportLogistics.Model;
using TransportLogistics.ViewModels.Drivers;

namespace TransportLogistics.Controllers
{
    public class DriversController : Controller
    {
        public DriversController(UserManager<IdentityUser> userManager,DriverService driverService,OrderService orderService,
            ILogger<DriversController> logger)
        {
            UserManager = userManager;
            DriverService = driverService;
            OrderService = orderService;
            Logger = logger;
        }

        private UserManager<IdentityUser> UserManager;

        private DriverService DriverService;

        private OrderService OrderService;
        private ILogger Logger;

        public async Task<IActionResult> Index()
        
        {
           
            var user = await UserManager.GetUserAsync(User);
            try
            {
                var driver = DriverService.GetByUserId(user.Id);
                var routeEntries = DriverService.GetRouteEntries(driver.Id);

                var currentRoute = new CurrentRouteViewModel();
                currentRoute.RouteEntries = routeEntries;
                currentRoute.DriverId = driver.Id;

                return View(currentRoute);
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to retrieve driver's route entries {@Exception}", e);
                Logger.LogError("Failed to retrieve driver's route entries{Exception}", e.Message);
                return BadRequest();
            }
        }
        public IActionResult SetOrderStatus(OrderStatus status , Guid orderId)
        {

            try
            {
                OrderService.ChangeOrderStatus(orderId, status);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to set order status {@Exception}", e);
                Logger.LogError("Failed to set order status {Exception}", e.Message);
                return BadRequest();
            }

        }
        public async Task<IActionResult> GetOrdersPartial()
        {
            var user = await UserManager.GetUserAsync(User);
            try
            {
                var driver = DriverService.GetByUserId(user.Id);
                var routeEntries = DriverService.GetRouteEntries(driver.Id);

                var currentRoute = new CurrentRouteViewModel();
                currentRoute.RouteEntries = routeEntries;
                currentRoute.DriverId = driver.Id;
                return PartialView("_OrdersTablePartial", currentRoute);
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to retrieve driver's route entries {@Exception}", e);
                Logger.LogError("Failed to retrieve driver's route entries {Exception}", e.Message);
                return BadRequest();
            }

        }
        public IActionResult EndRoute()
        {
            var user = UserManager.GetUserAsync(User).GetAwaiter().GetResult();
            try
            {
                var driver = DriverService.GetByUserId(user.Id);
                DriverService.EndCurrentRoute(driver);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to end route {@Exception}", e);
                Logger.LogError("Failed to end route {Exception}", e.Message);
                return BadRequest();
            }
        }
        public async Task<IActionResult> StartRoute()
        {
            var user = await UserManager.GetUserAsync(User);
            try
            {
                var driver = DriverService.GetByUserId(user.Id);
                DriverService.SetDriverStatus(driver, DriverStatus.Driving);
                return RedirectToAction("GetOrdersPartial");
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to retrieve users accounts {@Exception}", e);
                Logger.LogError("Failed to retrieve users accounts {Exception}", e.Message);
                return BadRequest();
            }
        }
        public async Task<IActionResult> RoutesHistory()
        {
            return View();
        }
        public async Task<IActionResult> CancelOrder()
        {
            return RedirectToAction("GetOrdersPartial");
        }
    }
}