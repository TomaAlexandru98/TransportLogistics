using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportLogistics.Models.Trailers
{
    public class TrailersViewModel
    {
        //public List<Models.Trailers.TrailersViewModel> TrailerList{ get; set; }
        //public Guid Id { get; set; }
        public int MaximWeightKg { get; protected set; }
        public string Model { get; protected set; }
        public int Capacity { get; protected set; }
        public int NumberAxles { get; protected set; }
        public decimal Height { get; protected set; }
        public decimal Width { get; protected set; }
        public decimal Length { get; protected set; }
    }
}
