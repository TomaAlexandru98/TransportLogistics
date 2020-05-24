using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransportLogistics.ApplicationLogic.Services;

namespace TransportLogistics.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly SupervisorService supervisorService;
        private readonly UserManager<IdentityUser> userManager;

        public SupervisorController(SupervisorService supervisorService,
                                    UserManager<IdentityUser> userManager)
        {
            this.supervisorService = supervisorService;
            this.userManager = userManager;
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
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult ManageRequests()
        {
            try
            {
                return View();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}