using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Data.Abstractions;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
    public class TrailerService
    {
        private readonly IPersistenceContext persistenceContext;
        private readonly ITrailersRepository trailersRepository;
        private readonly IVehiclesRepository vehiclesRepository;
        public TrailerService(IPersistenceContext persistenceContext,
                           IVehiclesRepository vehiclesRepository)
        {
            this.vehiclesRepository = vehiclesRepository;

            this.persistenceContext = persistenceContext;
        }

        public Trailer GetTrailerById(string Id)
        {

            Guid idToSearch = Guid.Parse(Id);
            var trailer = trailersRepository?.GetTrailerById(idToSearch);
            if (trailer == null)
            {
                throw new Exception();
            }

            return trailer;
        }

        public IEnumerable<Trailer> GetTrailerByVehicleId(string Id)
        {
            Guid idToSearch = Guid.Parse(Id);
            var trailers = trailersRepository.GetByVehicleId(idToSearch);
            if (trailers == null)
            {
                throw new Exception();
            }
            return trailers;
        }

    }
}
