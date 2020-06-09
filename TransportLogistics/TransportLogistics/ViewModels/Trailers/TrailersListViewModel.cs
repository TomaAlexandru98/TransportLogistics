using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Trailers
{
    public class TrailersListViewModel
    {
        public IEnumerable<Trailer> TrailerViews { get; set; }
    }
}
