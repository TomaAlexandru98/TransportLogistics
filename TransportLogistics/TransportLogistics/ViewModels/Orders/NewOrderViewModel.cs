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
    public class NewOrderViewModel : IValidatableObject
    {
        public enum LocationType
        {
            Delivery,
            Pickup
        }

        [Required(ErrorMessage = "Choosing a sender is required.")]
        [Display(Name = "Sender:")]
        public string SenderId { get; set; }

        //[Required(ErrorMessage = "Pickup Address is required.")]
        [Display(Name = "Pickup Address:")]
        public string PickupLocationId { get; set; }

        //[Required(ErrorMessage = "Delivery Address is required.")]
        [Display(Name = "Delivery Address:")]
        public string DeliveryLocationId { get; set; }

        [Required]
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


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PickupLocationId == null && DeliveryLocationId == null)
            {
                yield return new ValidationResult("At least one of them must have a value");
            }
        }
    }
}
