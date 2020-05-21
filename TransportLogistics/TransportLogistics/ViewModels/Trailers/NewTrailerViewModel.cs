using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.Models.Trailers
{
    public class NewTrailerViewModel
    {
        [Required]
        
        public Guid TrailerId { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value cannot be 0 ")]
        
        
        [Required]
        [Display(Name = "MaximWeight")]
        public int MaximWeightKg { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value cannot be 0 ")]
        [Required]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value cannot be 0 ")]
        [Required]
        [Display(Name = "NumberAxles")]
        public int NumberAxles { get; set; }

        [Range(0.1, 9999999, ErrorMessage = "Value cannot be 0 ")]
        [Required]
        [Display(Name = "Height")]
        public decimal Height { get; set; }

        [Range(0.1, 9999999, ErrorMessage = "Value cannot be 0 ")]
        [Required]
        [Display(Name = "Width")]
        public decimal Width { get; set; }

        [Range(0.1, 9999999, ErrorMessage = "Value cannot be 0 ")]
        [Required]
        [Display(Name = "Length")]
        public decimal Length { get; set; }

    }
}
