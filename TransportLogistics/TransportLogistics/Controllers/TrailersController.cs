using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly UserManager<IdentityUser> userManager;
        private readonly TrailerService trailerService;
        public TrailersController(IConfiguration configuration,TrailerService trailerService)
        {
            this.trailerService = trailerService;
            _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<TransportLogisticsDbContext>();
            _connectionString = _configuration.GetConnectionString("DefaultConnection1");
            _optionsBuilder.UseSqlServer(_connectionString);
        }
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
            var trailerid = Guid.NewGuid();

            return RedirectToAction("Index");
            //return PartialView("_NewTrailerPartial", new NewTrailerViewModel { TrailerId = trailerid });
        }

        [HttpPost]
        public IActionResult NewTrailer([FromForm]NewTrailerViewModel trailerData)
        {
            

           // trailerService.CreateTrailer(trailerData.Model, trailerData.MaximWeightKg, trailerData.Capacity, trailerData.NumberAxles, trailerData.Height, trailerData.Width, trailerData.Length);
            return RedirectToAction("Index");
            //return PartialView("_NewTrailerPartial", viewModelResult);
        }
    }
}