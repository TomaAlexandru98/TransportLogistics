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
        public string UpdateId { get; set; }

        [Required(ErrorMessage = "Pickup Address is required.")]
        [Display(Name = "Pickup Address:")]

        public string UpPickupLocationId { get; set; }

        [Required(ErrorMessage = "Delivery Address is required.")]
        [Display(Name = "Delivery Address:")]
        public string UpDeliveryLocationId { get; set; }

        public List<SelectListItem> PickupLocation { get; set; }
        public List<SelectListItem> DeliveryLocation { get; set; }

        [Range(0, 9999999, ErrorMessage = "Wrong Input.")]
        [Required]
        [Display(Name = "Price")]
        public decimal UpPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UpPickupLocationId == null || UpDeliveryLocationId == null || UpPickupLocationId == UpDeliveryLocationId)
            {
                yield return new ValidationResult("Pickup location can't be the same as delivery location");
            }
        }
        
    }
}
