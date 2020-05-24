using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.ViewModels.Requests;

namespace TransportLogistics.Controllers
{
    public class RequestsController : Controller
    {
        private readonly RequestService requestService;

        public RequestsController(RequestService requestService)
        {
            this.requestService = requestService;
        }

        public IActionResult Index()
        {
            try
            {
                var viewModel = new RequestsViewModel
                {
                    Requests = requestService.GetAll()
                };

                return View(viewModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}