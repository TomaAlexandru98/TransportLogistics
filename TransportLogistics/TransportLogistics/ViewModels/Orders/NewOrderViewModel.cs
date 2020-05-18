using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;
using TransportLogistics.ViewModels.Orders;
namespace TransportLogistics.ViewModels.Orders
{
    public class NewOrderViewModel
    {
        public string RecipientId { get; set; }
        public string Location1Id { get; set; }
        public string Location2Id { get; set; }

        public List<SelectListItem> CustomerList { get; set; }
        
        public List<SelectListItem> Location1 { get; set; }
        public List<SelectListItem> Location2 { get; set; }
        /*
        [Required(ErrorMessage = "PickUpAddress is required.")]
        [Display(Name = "PickUpAddress")]
        public LocationAddress PickUpAddress { get; set; }

        [Required(ErrorMessage = "DeliveryAddress is required.")]
        [Display(Name = "DeliveryAddress")]
        public LocationAddress DeliveryAddress { get; set; }
        */
     /*
        [Required(ErrorMessage = "Recipient is required.")]
        [Display(Name = "Recipient")]
        public string RecipientId { get; set; }
    */
  
        [Range(0, 9999999, ErrorMessage = "Wrong Input.")]
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
