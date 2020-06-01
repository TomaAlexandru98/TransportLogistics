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

        public IEnumerable<Request> GetAllActive()
        {
            return requestRepository?.GetAllActive();
        }

        public Request GetById(string id)
        {
            var requestId = Guid.Parse(id);
            return requestRepository?.GetById(requestId);
        }

        public void Decline(string id, Supervisor supervisor)
        {
            var requestToDecline = GetById(id);
            requestToDecline.SetStatus(RequestStatus.Declined);
            requestToDecline.SetSupervisor(supervisor);
            requestRepository?.Update(requestToDecline);
            persistenceContext.SaveChanges();
        }

        public void Accept(string id, Supervisor supervisor)
        {
            var requestToAccept = GetById(id);
            requestToAccept.SetStatus(RequestStatus.Accepted);
            requestToAccept.SetSupervisor(supervisor);
            requestRepository?.Update(requestToAccept);
            persistenceContext.SaveChanges();
        }

        public IEnumerable<Request> FilterByTrailerId(Guid trailerId)
        {
            return requestRepository?.FilterByTrailerId(trailerId);
        }

        public void Create(Guid senderId,Vehicle vehicle,Trailer trailer)
        {
            var request = Request.Create(senderId, vehicle, trailer);
            requestRepository.Add(request);
            persistenceContext.SaveChanges();
        }
    }
}
