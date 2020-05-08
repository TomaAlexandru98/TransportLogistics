using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.Models.Customers
{
    public class UpdateCustomerViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name missing!")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "PhoneNumber missing!")]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Email missing!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
