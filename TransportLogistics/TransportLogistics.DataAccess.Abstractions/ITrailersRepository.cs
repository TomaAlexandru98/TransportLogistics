using System;
using System.Collections.Generic;
using System.Text;
namespace TransportLogistics.Data.Abstractions
{
    public interface ITrailersRepository : IBaseRepository<TrailerData>
    {
        
        TrailerData GetTrailerById(Guid trailerId);
        IEnumerable<TrailerData> GetByVehicleId(Guid vehicleId);
    }
}
