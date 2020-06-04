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
                var viewModel = new ConnectRequestsViewModel
                {
                    ShowMultipleRequestsModal = true,
                    Requests = requestService.GetAllConnectActive()
                };

                return View(viewModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult ConnectRequestsTable()
        {
            try
            {
                var requestsList = requestService.GetAllConnectActive();

                var viewModel = new ConnectRequestsViewModel
                {
                    AreRequestsActive = true,
                    ShowMultipleRequestsModal = true,
                    Employees = requestService.GetAllSenders(requestsList),
                    Requests = requestsList.OrderBy(request => request.Date)
                };

                return PartialView("_ConnectRequestsTable", viewModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult DepartureRequestsTable()
        {
            try
            {
                var viewModel = new DepartureRequestsViewModel
                {
                    AreRequestsActive = true,
                    DepartureRequests = requestService.GetAllDepartureActive()
                };

                return PartialView("_DepartureRequestsTable", viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult DeclineConnect(string id)
        {
            var supervisorId = userManager.GetUserId(User);

            try
            {
                var supervisorDb = supervisorService.GetByUserId(supervisorId);
                requestService.DeclineConnect(id, supervisorDb);

                return RedirectToAction("Index");
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
                requestService.DeclineDeparture(id, supervisorDb);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult AcceptConnect(string id)
        {
            var supervisorId = userManager.GetUserId(User);

            try
            {
                var supervisorDb = supervisorService.GetByUserId(supervisorId);
                requestService.AcceptConnect(id, supervisorDb);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult AcceptDeparture(string id)
        {
            var supervisorId = userManager.GetUserId(User);

            try
            {
                var supervisorDb = supervisorService.GetByUserId(supervisorId);
                requestService.AcceptDeparture(id, supervisorDb);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult Filter(string id)
        {
            try
            {
                var requestDb = requestService.GetById(id);
                var viewModel = new ConnectRequestsViewModel
                {
                    ShowMultipleRequestsModal = false,
                    Requests = requestService.FilterByTrailerId(requestDb.Trailer.Id)
                };

                return PartialView("_ConnectRequestsTable", viewModel);
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

        public IActionResult ConnectHistory()
        {
            try
            {
                var requestsList = requestService.GetConnectHistory().OrderBy(request => request.Date);
                var viewModel = new ConnectRequestsViewModel
                {
                    AreRequestsActive = false,
                    Employees = requestService.GetAllSenders(requestsList),
                    Requests = requestsList
                };

                return PartialView("_ConnectRequestsTable", viewModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult DepartureHistory()
        {
            try
            {
                var viewModel = new DepartureRequestsViewModel
                {
                    AreRequestsActive = false,
                    DepartureRequests = requestService.GetDepartureHistory().OrderBy(request => request.Date)
                };

                return PartialView("_DepartureRequestsTable", viewModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}