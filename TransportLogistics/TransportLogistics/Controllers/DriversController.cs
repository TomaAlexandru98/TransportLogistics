using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.ApplicationLogic.Sevices;
using TransportLogistics.Model;
using TransportLogistics.ViewModels.Drivers;

namespace TransportLogistics.Controllers
{
    public class DriversController : Controller
    {
        public DriversController(UserManager<IdentityUser> userManager,DriverService driverService,OrderService orderService)
        {
            UserManager = userManager;
            DriverService = driverService;
            OrderService = orderService;
        }

        private UserManager<IdentityUser> UserManager;

        private DriverService DriverService;

        private OrderService OrderService;

        public async Task<IActionResult> Index()
        
        {
            var user = await UserManager.GetUserAsync(User);
            var driver = DriverService.GetByUserId(user.Id);
            var routeEntries = DriverService.GetRouteEntries(driver.Id);
           
            var currentRoute = new CurrentRouteViewModel();
            currentRoute.RouteEntries = routeEntries;
            currentRoute.DriverId = driver.Id;

            return View(currentRoute);
        }
        public IActionResult SetOrderStatus(OrderStatus status , Guid orderId,Guid driverId)
        {

            OrderService.ChangeOrderStatus(orderId, status);
            return RedirectToAction("Index");
           
        }
        public async Task<IActionResult> GetOrdersPartial()
        {
            var user = await UserManager.GetUserAsync(User);
            var driver = DriverService.GetByUserId(user.Id);
            var routeEntries = DriverService.GetRouteEntries(driver.Id);

            var currentRoute = new CurrentRouteViewModel();
            currentRoute.RouteEntries = routeEntries;
            currentRoute.DriverId = driver.Id;
            return PartialView("_OrdersTablePartial", currentRoute);

        }
        public IActionResult EndRoute()
        {
            var user = UserManager.GetUserAsync(User).GetAwaiter().GetResult();
            var driver = DriverService.GetByUserId(user.Id);
            DriverService.EndCurrentRoute(driver);
            return RedirectToAction("Index");
        }
    }
}