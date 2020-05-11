using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.DataAccess;
using TransportLogistics.Model;
using TransportLogistics.Models.Trailers;

namespace TransportLogistics.Controllers
{
    public class TrailersController : Controller
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        DbContextOptionsBuilder<TransportLogisticsDbContext> _optionsBuilder;
        private readonly TrailerService trailerService;
        private readonly ILogger<TrailerService> logger;
        public TrailersController(IConfiguration configuration,TrailerService trailerService, ILogger<TrailerService> logger)
        {
            this.logger = logger;
            this.trailerService = trailerService;
            _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<TransportLogisticsDbContext>();
            _connectionString = _configuration.GetConnectionString("DefaultConnection1");
            _optionsBuilder.UseSqlServer(_connectionString);
        }
        [HttpGet]
        public IActionResult Index()
        {
            using (TransportLogisticsDbContext _context = new TransportLogisticsDbContext(_optionsBuilder.Options))
            {
                return View(_context.Trailers.ToList());
            }
           
        }

        [HttpGet]
        public IActionResult NewTrailer()
        {
            
            return PartialView("_NewTrailerPartial", new NewTrailerViewModel());
        }

        [HttpPost]
        public IActionResult NewTrailer([FromForm]NewTrailerViewModel trailerData)
        {
           
            try
            {
                if(ModelState.IsValid)
                {
                    trailerService.CreateTrailer(trailerData.Model, trailerData.MaximWeightKg, trailerData.Capacity, trailerData.NumberAxles, trailerData.Height, trailerData.Width, trailerData.Length);
                    //return RedirectToAction("Index");
                    return PartialView("_NewTrailerPartial", trailerData);
                }
                return View(trailerData);
               
            }
            catch (Exception e)
            {
                logger.LogError("Failed to create a new Trailer {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new Trailer {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
            public IActionResult EditTrailer([FromRoute]string id)
        {
            var trailer = trailerService.GetTrailerById(id);
            NewTrailerViewModel model = new NewTrailerViewModel() {
                TrailerId = trailer.Id,
                Capacity = trailer.Capacity,
                MaximWeightKg = trailer.MaximWeightKg,
                Model = trailer.Model,
                NumberAxles = trailer.NumberAxles,
                Height = trailer.Height,
                Width = trailer.Width,
                Length = trailer.Length
            }
            ;
            
            return PartialView("_NewTrailerPartial",model);
          
        }
        
        [HttpPost]
        public ActionResult EditTrailer([FromForm]NewTrailerViewModel trailerData)
        {
            //trailerserveice.Updatedata
            var trailer = trailerService.GetTrailerById(trailerData.TrailerId.ToString());
            //trailer.Modify(trailer, trailerData.Model, trailerData.MaximWeightKg, trailerData.Capacity, trailerData.NumberAxles, trailerData.Height, trailerData.Width, trailerData.Length);
            trailerService.Update(trailerData.TrailerId, trailerData.Model, trailerData.MaximWeightKg, trailerData.Capacity, trailerData.NumberAxles, trailerData.Height, trailerData.Width, trailerData.Length);
            return PartialView("_NewTrailerPartial", trailerData);
        }

        [HttpGet]
        public IActionResult Remove([FromRoute]string Id)
        {

            RemoveTrailerViewModel removeViewModel = new RemoveTrailerViewModel()
            {
                Id = Id
            };

            return PartialView("_RemoveTrailerPartial", removeViewModel);
        }

        [HttpPost]
        public IActionResult Remove(RemoveTrailerViewModel removeData)
        {
            
                trailerService.RemoveTrailer(removeData.Id);


            return PartialView("_RemoveTrailerPartial", removeData);
        }
    }
}