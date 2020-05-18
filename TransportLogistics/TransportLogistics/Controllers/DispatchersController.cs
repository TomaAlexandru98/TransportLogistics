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
    public class DispatchersController : Controller
    {

        private readonly DriverService driverService;
        private readonly ILogger<DriverService> logger;

        public DispatchersController(DriverService driverService, ILogger<DriverService> logger)
        {
            this.driverService = driverService;
            this.logger = logger;
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
    }
}