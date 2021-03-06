﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.ViewModels.Customers
{
    public class EditLocationViewModel
    {
        public string Id { get; set; }

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
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
    }
}
