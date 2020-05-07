using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.Models.Customers
{
    public class NewLocationViewModel
    {

        [Required(ErrorMessage = "Country required")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street required")]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "Street number")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "Postal Code required")]
        [Display(Name = "PostalCode")]
        public string PostalCode { get; set; }

    }
}
