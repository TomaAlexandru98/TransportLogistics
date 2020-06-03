using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.ViewModels.Requests;

namespace TransportLogistics.Controllers
{
    public class RequestsController : Controller
    {
        private readonly RequestService requestService;
        private readonly SupervisorService supervisorService;
        private readonly UserManager<IdentityUser> userManager;

        public RequestsController(RequestService requestService,
            UserManager<IdentityUser> userManager,
            SupervisorService supervisorService)

        {
            this.requestService = requestService;
            this.userManager = userManager;
            this.supervisorService = supervisorService;
        }

        public IActionResult Index()
        {
            try
            {
                var viewModel = new RequestsViewModel
                {
                    ShowMultipleRequestsModal = true,
                    Requests = requestService.GetAllActive()
                };

                return View(viewModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult RequestsTable()
        {
            try
            {
                var viewModel = new RequestsViewModel
                {
                    ShowMultipleRequestsModal = true,
                    Requests = requestService.GetAllActive()
                };

                return PartialView("_RequestsTable", viewModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult Decline(string id)
        {
            var supervisorId = userManager.GetUserId(User);

            try
            {
                var supervisorDb = supervisorService.GetByUserId(supervisorId);
                requestService.Decline(id, supervisorDb);

                return RedirectToAction("Index");
                //return PartialView("_RequestsTable");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult Accept([FromQuery] string id)
        {
            var supervisorId = userManager.GetUserId(User);

            try
            {
                var supervisorDb = supervisorService.GetByUserId(supervisorId);
                requestService.Accept(id, supervisorDb);

                return RedirectToAction("Index");
                //return PartialView("_RequestsTable");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult Filter(string id)
        {
            try
            {
                var requestDb = requestService.GetById(id);
                var x = requestService.FilterByVehicleId(requestDb.Vehicle.Id);
                var viewModel = new RequestsViewModel
                {
                    ShowMultipleRequestsModal = false,
                    Requests = requestService.FilterByVehicleId(requestDb.Vehicle.Id)
                };

                return PartialView("_RequestsTable", viewModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult ConnectTrailerToVehicle(string vehicleId, string trailerId)
        {
            var userId = userManager.GetUserId(User);

            try
            {
                var supervisorDb = supervisorService.GetByUserId(userId);
                supervisorService.ConnectTrailerToVehicle(supervisorDb.Id, vehicleId, trailerId);
                return RedirectToAction("Index", "Vehicles");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}