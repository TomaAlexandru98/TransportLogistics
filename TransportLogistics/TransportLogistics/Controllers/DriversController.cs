using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransportLogistics.ApplicationLogic.Sevices;
using TransportLogistics.ViewModels.Drivers;

namespace TransportLogistics.Controllers
{
    public class DriversController : Controller
    {
        public DriversController(UserManager<IdentityUser> userManager,DriverService driverService)
        {
            UserManager = userManager;
            DriverService = driverService;
        }

        private UserManager<IdentityUser> UserManager;

        private DriverService DriverService;

        public async Task<IActionResult> Index()
        {
            var user = await UserManager.GetUserAsync(User);
            var driver = DriverService.GetByUserId(user.Id);
            var orders = DriverService.GetFilteredOrders(driver.Id);

            //DriverService.AddNewRoute(driver.Id, orders);
            var currentRoute = new CurrentRouteViewModel();
            currentRoute.Orders = orders;
            currentRoute.DriverId = driver.Id;

            return View(currentRoute);
        }

    }
}