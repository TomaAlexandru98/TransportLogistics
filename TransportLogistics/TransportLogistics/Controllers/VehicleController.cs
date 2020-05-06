using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.ViewModels.Vehicles;

namespace TransportLogistics.Controllers
{
    public class VehicleController : Controller
    {
        private readonly VehicleService vehicleService;
        private readonly ILogger<VehicleService> logger;

        public VehicleController(VehicleService vehicleService, ILogger<VehicleService> logger)
        {
            this.vehicleService = vehicleService;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var viewModel = new VehiclesListViewModel
                {
                    Vehicles = vehicleService.GetAll()
                };
                return View(viewModel);
            }
            catch(Exception e)
            {
                logger.LogError("Failed to retrieve vehicle list {@Exception}", e.Message);
                logger.LogDebug("Failed to retrieve vehicle list {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        public IActionResult VehiclesTable()
        {
            try
            {
                var viewModel = new VehiclesListViewModel
                {
                    Vehicles = vehicleService.GetAll()
                };
                return PartialView("_VehiclesTable", viewModel);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to retrieve vehicle list {@Exception}", e.Message);
                logger.LogDebug("Failed to retrieve vehicle list {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create", new NewVehicleViewModel());
        }

        [HttpPost]
        public IActionResult Create([FromForm]NewVehicleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", new NewVehicleViewModel());
            }

            try
            {
                vehicleService.Add(viewModel.Name,
                                   viewModel.Type,
                                   viewModel.RegistrationNumber,
                                   viewModel.MaximCarryWeight,
                                   viewModel.VIN);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                logger.LogError("Failed to create a new vehicle {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new vehicle {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        public IActionResult Remove(string id)
        {
            try
            {
                vehicleService.Remove(id);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                logger.LogError("Failed to remove a vehicle {@Exception}", e.Message);
                logger.LogDebug("Failed to remove a vehicle {ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            try
            {
                var vehicleFromDb = vehicleService.GetById(id);
                var viewModel = new UpdateVehicleViewModel
                {
                    Id = id,
                    Name = vehicleFromDb.Name,
                    Type = vehicleFromDb.Type,
                    RegistrationNumber = vehicleFromDb.RegistrationNumber,
                    MaximCarryWeight = vehicleFromDb.MaximCarryWeightKg,
                    VIN = vehicleFromDb.VIN
                };
                return PartialView("_Update", viewModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Update([FromForm] UpdateVehicleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Update");
            }

            try
            {
                vehicleService.Update(viewModel.Id,
                                      viewModel.Name,
                                      viewModel.Type,
                                      viewModel.RegistrationNumber,
                                      viewModel.MaximCarryWeight,
                                      viewModel.VIN);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}