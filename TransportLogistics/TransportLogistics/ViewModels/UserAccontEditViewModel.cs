using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.ViewModels
{
    public class UserAccontEditViewModel
    {
        [Required]
        
        public string PhoneNumber { get; set; }
        [Required]
        
        public string Email { get; set; }
        [Required]
       
        public string Name { get; set; }
        [Required]
        
        public string UserId { get; set; }
        [Required]
        
        public string Role { get; set; }

    }
}
