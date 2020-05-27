using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.Data;
using TransportLogistics.Data.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFTrailerRepository:EFBaseRepository<Trailer>,ITrailerRepository
    {
        public EFTrailerRepository(TransportLogisticsDbContext dbContext) : base(dbContext)
        {

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

        public Trailer UpdateTrailer(Guid trailerId, string model, int maximumWeightKg, int capacity, int numberAxles, decimal height, decimal width, decimal length)
        {
            var targettrailer = dbContext.Trailers.Find(trailerId);
            
            targettrailer.Modify(targettrailer, model, maximumWeightKg, capacity, numberAxles, height, width, length);
            dbContext.Update(targettrailer);
            dbContext.SaveChanges();
            return targettrailer;
        }

        public new IEnumerable<Trailer> GetAll()
        {
            return dbContext.Trailers.AsEnumerable();
        }
        public IEnumerable<Trailer> GetAllFreeTrailers()
        {
            return  dbContext.Trailers.Where(o => o.Status == Status.Free);
        }

        public Trailer GetByRegistrationNumber(string registrationNumber)
        {
            return dbContext.Trailers.Where(o => o.RegistrationNumber == registrationNumber).FirstOrDefault();
        }
    }
}
