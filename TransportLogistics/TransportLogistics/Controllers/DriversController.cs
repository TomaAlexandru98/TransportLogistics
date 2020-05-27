using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.Model;
using TransportLogistics.ViewModels.Drivers;

namespace TransportLogistics.Controllers
{
    public class DriversController : Controller
    {
        public DriversController(UserManager<IdentityUser> userManager,DriverService driverService,OrderService orderService,
            ILogger<DriversController> logger,TrailerService trailerService)
        {
            UserManager = userManager;
            DriverService = driverService;
            OrderService = orderService;
            TrailerService = trailerService;
            Logger = logger;
        }

        private UserManager<IdentityUser> UserManager;

        private DriverService DriverService;

        private OrderService OrderService;
        private ILogger Logger;
        private TrailerService TrailerService;

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
            var user = await UserManager.GetUserAsync(User);
            try
            {
                var driver = DriverService.GetByUserId(user.Id);
                var routesHistoric = DriverService.GetRoutesHistory(driver.Id);
                var routesHistoricViewModel = new RoutesHistoryViewModel();
                routesHistoricViewModel.ConfigureRoutes(routesHistoric.Routes);
                return View(routesHistoricViewModel);
            }
            catch(Exception e)
            {
                Logger.LogDebug("Failed to retrieve routes for current driver {@Exception}", e);
                Logger.LogError("Failed to retrieve routes for current driver {Exception}", e.Message);
                return BadRequest();
            }
        }
        public IActionResult Route(Guid id)
        {
            try
            {
                var route = DriverService.GetRouteById(id);

                return View(route);
            }
            catch(Exception e)
            {
                Logger.LogDebug("Failed to retrieve route  {@Exception}", e);
                Logger.LogError("Failed to retrieve route  {Exception}", e.Message);
                return BadRequest();
            }
        }  
        public IActionResult Trailers()
        {
            try
            {
                var trailers = TrailerService.GetAllFreeTrailers();
                return View(trailers);
            }
            catch(Exception e)
            {
                Logger.LogDebug("Failed to retrieve available trailers  {@Exception}", e);
                Logger.LogError("Failed to retrieve available trailers  {Exception}", e.Message);
                return BadRequest();
            }
        }
        public async Task<IActionResult> TrailerRequest(string registrationNumber)
        {
            try
            {
                var user = await UserManager.GetUserAsync(User);
                var driver = DriverService.GetByUserId(user.Id);
                DriverService.CreateRequest(driver.Id, registrationNumber);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Logger.LogDebug("Failed to make trailer request  {@Exception}", e);
                Logger.LogError("Failed to make trailer request  {Exception}", e.Message);
                return BadRequest();
            }

        }


        public IActionResult DriversTable()
        {
            var x = DriverService.GetAllDrivers();
            try
            {
                var viewModel = new DriversViewModel
                {
                    Drivers = DriverService.GetAllDrivers()
                };

                return View(viewModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult DetailsDriver(string id)
        {
            try
            {
                var driverId = Guid.Parse(id);

                var viewModel = new RoutesHistoryViewModel{};
                viewModel.ConfigureRoutes(DriverService.GetRoutesHistory(driverId).Routes);

                return View("RoutesHistory", viewModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        public async Task<IActionResult> Map()
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
        public IActionResult RouteMap()
        {
            return View();

        }
    }
}