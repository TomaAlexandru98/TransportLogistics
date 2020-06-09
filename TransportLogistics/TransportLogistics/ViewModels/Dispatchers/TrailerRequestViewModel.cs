using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Dispatchers
{
    public class TrailerRequestViewModel
    {
       public TrailerRequestViewModel()
        {
            
        }
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<Trailer> Trailers { get; set; }
    }
}
