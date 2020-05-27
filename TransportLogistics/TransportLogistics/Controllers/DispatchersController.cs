using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DispatchersController(DriverService driverService, ILogger<DriverService> logger,TrailerService trailerService
            ,VehicleService vehicleService)
        {
            this.driverService = driverService;
            this.logger = logger;
            this.trailerService = trailerService;
            this.vehicleService = vehicleService;
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
        public IActionResult TrailerRequests(string vehicleNumber, string trailerNumber)
        {
            return RedirectToAction("Index");
        }
      
    }
}