using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.Data;
using TransportLogistics.Data.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFTrailerRepository:EFBaseRepository<Trailer>,ITrailersRepository
    {
        public EFTrailerRepository(TransportLogisticsDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Trailer> GetByVehicleId(Guid vehicleId)
        {
            var targetvehicle = dbContext.Vehicles.Where(vehicle => vehicle.Id == vehicleId).FirstOrDefault();
            var trailerlist = targetvehicle.CurrentTrailers;
            return trailerlist;
        }

        public Trailer GetTrailerById(Guid trailerId)
        {
            var Trailer = dbContext.Trailers.Where(trailer => trailer.Id == trailerId).FirstOrDefault();
            return Trailer;
        }
        public Trailer UpdateTrailer(Guid trailerId,Trailer details)
        {
            var trailer = dbContext.Update(details);
            dbContext.SaveChanges();
            return trailer.Entity;
        }
    }
}
