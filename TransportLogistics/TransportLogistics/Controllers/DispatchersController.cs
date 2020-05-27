using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.ViewModels.Dispatchers;

namespace TransportLogistics.Controllers
{
    public class DispatchersController : Controller   //Delete and use other controllers sau exclusive pentru el
    {

        private readonly DriverService driverService;
        private readonly ILogger<DriverService> logger;
        private readonly TrailerService trailerService;
        private readonly VehicleService vehicleService;
        private readonly RequestService requestService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly DispatcherService dispatcherService;
        public DispatchersController(DriverService driverService, ILogger<DriverService> logger,TrailerService trailerService
            ,VehicleService vehicleService,RequestService requestService , UserManager<IdentityUser>userManager,DispatcherService dispatcherService)
        {
            this.driverService = driverService;
            this.logger = logger;
            this.trailerService = trailerService;
            this.vehicleService = vehicleService;
            this.requestService = requestService;
            this.userManager = userManager;
            this.dispatcherService = dispatcherService;
        }

        public IActionResult Index()

        {
            return View();
        }

        public IActionResult DriversTable()
        {
            try
            {
                var driversViewModel = new DriversListViewModel()
                {
                    Drivers = driverService.GetAllDrivers()
                };

                return PartialView("_DriversTablePartial", driversViewModel);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to load Driver entities {@Exception}", e.Message);
                logger.LogDebug("Failed to load Driver entities {@ExceptionMessage}", e);
                return BadRequest("Failed to load Driver entities");
            }

        }
        
        public IActionResult TrailerRequest()
        {
            try
            {
                var vehicles = vehicleService.GetAll();
                var trailers = trailerService.GetAllFreeTrailers();
                var model = new TrailerRequestViewModel()
                {
                    Trailers = trailers,
                    Vehicles = vehicles
                };
                return View(model);
            }
            catch(Exception e)
            {
                logger.LogError("Failed to retrieve trailers or vehicles {@Exception}", e.Message);
                logger.LogDebug("Failed to retrieve trailers or vehicles {@ExceptionMessage}", e);
                return BadRequest("Failed to retrieve trailers or vehicles");
            }
        }
        [HttpPost]
        public IActionResult TrailerRequest(string vehicleNumber, string trailerNumber)
        {
            try
            {
                var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var dispatcher = dispatcherService.GetByUserId(user.Id);
                var vehicle = vehicleService.GetByRegistrationNumber(vehicleNumber);
                var trailer = trailerService.GetByRegistrationNumber(trailerNumber);
                requestService.Create(dispatcher.Id, vehicle, trailer);
            }
            catch(Exception e)
            {
                logger.LogError("Unable to create trailer request {@Exception}", e.Message);
                logger.LogDebug("Unable to create trailer request {@ExceptionMessage}", e);
                return BadRequest("Unable to create trailer request");
            }
            return RedirectToAction("Index");
        }
      
    }
}