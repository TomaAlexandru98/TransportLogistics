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

        /*
        [Required(ErrorMessage = "Country required")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street required")]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Street number")]
        public int StreetNumber { get; set; }

        [Required(ErrorMessage = "Postal Code required")]
        [Display(Name = "PostalCode")]
        public string PostalCode { get; set; }
        */
        


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
        public List<SelectListItem> PickupLocation { get; set; }
        public List<SelectListItem> DeliveryLocation { get; set; }

        [Range(0, 9999999, ErrorMessage = "Wrong Input.")]
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
