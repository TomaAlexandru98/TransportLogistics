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
        //private readonly IPersistenceContext persistenceContext;
        private readonly ITrailerRepository trailersRepository;
       
        public TrailerService(ITrailerRepository trailersRepository)
        {
            this.trailersRepository = trailersRepository;

           // this.persistenceContext = persistenceContext;
        }

        public Trailer CreateTrailer(string model, int maximumWeightKg, int capacity, int numberAxles, decimal height, decimal width, decimal length)
        {
            var trailer =  Trailer.Create(model, maximumWeightKg, capacity, numberAxles, height, width, length);
            trailersRepository.Add(trailer);
            
            //persistenceContext.SaveChanges();
            return trailer;
            
        }

        
        public Trailer GetTrailerById(string Id)
        {

            Guid idToSearch = Guid.Parse(Id);
            var trailer = trailersRepository.GetById(idToSearch);
            //var trailer = trailersRepository.GetTrailerById(idToSearch);
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

        public void RemoveTrailer(string id)
        {
            Guid idToSearch = Guid.Parse(id);
            trailersRepository.Remove(idToSearch);
        }

        public void Update(Guid id, string model, int maximumWeightKg, int capacity, int numberAxles, decimal height, decimal width, decimal length)
        {
           
            trailersRepository.UpdateTrailer( id,  model,  maximumWeightKg,  capacity,  numberAxles,  height,  width,  length);
        }
    }
}
