using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public TrailerData GetTrailerById(Guid trailerId)
        {
            throw new NotImplementedException();
        }
    }
}
