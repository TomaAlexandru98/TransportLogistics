using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
    public class RequestService
    {
        private readonly IPersistenceContext persistenceContext;
        private readonly IRequestRepository requestRepository;

        public RequestService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
            this.requestRepository = persistenceContext.RequestRepository;
        }

        public IEnumerable<Request> GetAll()
        {
            return this.requestRepository?.GetAll();
        }

        public Request GetById(string id)
        {
            var requestId = Guid.Parse(id);
            return this.requestRepository?.GetById(requestId);
        }
        public void Create(Guid senderId,Vehicle vehicle,Trailer trailer)
        {
            var request = Request.Create(senderId, vehicle, trailer);
            requestRepository.Add(request);
            persistenceContext.SaveChanges();
        }
    }
}
