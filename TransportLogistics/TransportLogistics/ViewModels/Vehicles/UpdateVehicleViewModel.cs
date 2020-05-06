using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.ViewModels.Vehicles
{
    public class UpdateVehicleViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Registration number")]
        public string RegistrationNumber { get; set; }

        [Required]
        [Display(Name = "Maxim carry weight")]
        public int MaximCarryWeight { get; set; }

        [Required]
        [Display(Name = "VIN(Vehicle Identification Number)")]
        public string VIN { get; set; }
    }
}
