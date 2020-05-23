﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportLogistics.Model;

namespace TransportLogistics.ViewModels.Trailers
{
    public class AvailableTrailersViewModel
    {
        public string VehicleId { get; set; }
        public IEnumerable<Trailer> Trailers { get; set; }
    }
}
