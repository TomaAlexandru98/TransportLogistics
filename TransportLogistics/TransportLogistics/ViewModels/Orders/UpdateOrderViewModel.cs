using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Serialization;

namespace TransportLogistics.ViewModels.Orders
{
    public class UpdateOrderViewModel : IValidatableObject
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Pickup Address is required.")]
        [Display(Name = "Pickup Address:")]

        public string PickupLocationId { get; set; }

        [Required(ErrorMessage = "Delivery Address is required.")]
        [Display(Name = "Delivery Address:")]
        public string DeliveryLocationId { get; set; }

        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> PickupLocation { get; set; }
        public List<SelectListItem> DeliveryLocation { get; set; }

        [Range(0, 9999999, ErrorMessage = "Wrong Input.")]
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }


        //TODO: Client-side validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PickupLocationId == null || DeliveryLocationId == null || PickupLocationId == DeliveryLocationId)
            {
                yield return new ValidationResult("Pickup location can't be the same as delivery location");
            }
        }
        
    }
}
