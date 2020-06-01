using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Routes
{
    public class AddOrderViewModel
    {
        public string RouteId { get; set; }

        [Required(ErrorMessage = "Choosing an Order is required.")]
        [Display(Name = "Order:")]
        public string OrderId { get; set; }
        public List<SelectListItem> OrderList;

        [Required(ErrorMessage = "Choosing a type is required.")]
        [Display(Name = "Type:")]
        public OrderType type { get; set; }
        public List<SelectListItem> OrderType;

    }
}
