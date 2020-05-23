using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;
using TransportLogistics.ViewModels.Customers;

namespace TransportLogistics.ViewModels.Orders
{
    public class NewOrderViewModel
    {
        public enum LocationType
        {
            None,
            Delivery,
            Pickup
        }

        public string BonusLocationType { get; set; }

        [Required(ErrorMessage = "Choosing a sender is required.")]
        [Display(Name = "Sender:")]
        public string SenderId { get; set; }

        [Required(ErrorMessage = "Pickup Address is required.")]
        [Display(Name = "Pickup Address:")]
        public string PickupLocationId { get; set; }

        [Required(ErrorMessage = "Delivery Address is required.")]
        [Display(Name = "Delivery Address:")]
        public string DeliveryLocationId { get; set; }

        public NewLocationViewModel NewLocation {get; set; }

        [Required(ErrorMessage = "A recipient is required.")]
        [Display(Name = "Recipient Name")]
        public string RecipientName { get; set; }

        [Required(ErrorMessage = "A recipient email is required.")]
        [Display(Name = "Recipient Email")]
        public string RecipientEmail { get; set; }

        [Required(ErrorMessage = "A recipient phone number is required.")]
        [Display(Name = "Recipient Phone Number")]
        public string RecipientPhoneNo { get; set; }

        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> PickupLocations { get; set; }
        public List<SelectListItem> DeliveryLocations { get; set; }

        [Range(0, 9999999, ErrorMessage = "Wrong Input.")]
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
