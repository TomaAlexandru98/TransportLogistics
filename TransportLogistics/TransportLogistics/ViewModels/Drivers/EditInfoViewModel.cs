using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.ViewModels.Drivers
{
    public class EditInfoViewModel
    {
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name="PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
