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
        public Guid? TrailerId { get; set; }

        [Required(ErrorMessage = "The amount must be specified")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "MaximWeight")]
        public int MaximWeightKg { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Required]
        [Display(Name = "NumberAxles")]
        public int NumberAxles { get; set; }
        [Required]
        [Display(Name = "Height")]
        public decimal Height { get; set; }
        [Required]
        [Display(Name = "Width")]
        public decimal Width { get; set; }
        [Required]
        [Display(Name = "Length")]
        public decimal Length { get; set; }

    }
}
