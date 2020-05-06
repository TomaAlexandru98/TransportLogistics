﻿using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.Data.Abstractions
{
    public interface ITrailerRepository : IBaseRepository<Trailer>
    {
        
        Trailer GetTrailerById(Guid trailerId);
        IEnumerable<Trailer> GetByVehicleId(Guid vehicleId);
        Trailer UpdateTrailer(Guid trailerId, Trailer details);
    }
}