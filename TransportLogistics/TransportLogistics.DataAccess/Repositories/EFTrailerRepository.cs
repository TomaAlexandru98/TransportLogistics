using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.Data;
using TransportLogistics.Data.Abstractions;
namespace TransportLogistics.DataAccess.Repositories
{
    public class EFTrailerRepository:EFBaseRepository<TrailerData>,ITrailersRepository
    {
        public EFTrailerRepository(TransportLogisticsDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<TrailerData> GetByVehicleId(Guid vehicleId)
        {
            var trailerlist = dbContext.Trailers.Where(trailer => trailer.Vehicle.Id == vehicleId);
            return trailerlist;
        }

        public TrailerData GetTrailerById(Guid trailerId)
        {
            var Trailer = dbContext.Trailers.Where(trailer => trailer.Id == trailerId).FirstOrDefault();
            return Trailer;
        }
    }
}
