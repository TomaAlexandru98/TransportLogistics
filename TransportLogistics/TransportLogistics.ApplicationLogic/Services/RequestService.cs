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

        public RequestService(IPersistenceContext persistenceContext,
                              IRequestRepository requestRepository)
        {
            this.persistenceContext = persistenceContext;
            this.requestRepository = persistenceContext.RequestRepository;
        }

        public Request GetById(string id)
        {
            var requestId = Guid.Parse(id);
            return this.requestRepository?.GetById(requestId);
        }

        public IEnumerable<Request> GetAll()
        {
            return this.requestRepository?.GetAll();
        }

        public Request AddBySupervisor(string supervisorId, string driverId, string trailerId)
        {
            throw new Exception();
        }
    }
}
