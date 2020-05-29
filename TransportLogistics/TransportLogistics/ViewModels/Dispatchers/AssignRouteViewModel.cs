using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TransportLogistics.ViewModels.Dispatchers
{
    public class AssignRouteViewModel
    {
        public string DriverId { get; set; }
        [Required(ErrorMessage = "Choosing a route is required.")]
        [Display(Name = "Route:")]
        public string RouteId { get; set; }

        public List<SelectListItem> RouteList { get; set; }
    }
}
