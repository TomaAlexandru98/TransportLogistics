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
        private readonly IDriverRepository driverRepository;
        private readonly IDispatcherRepository dispatcherRepository;

        public RequestService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
            this.requestRepository = persistenceContext.RequestRepository;
            this.driverRepository = persistenceContext.DriverRepository;
            this.dispatcherRepository = persistenceContext.DispatcherRepository;
        }

        public IEnumerable<Request> GetAllConnectActive()
        {
            return requestRepository?.GetAllConnectActive();
        }

        public IEnumerable<DepartureRequest> GetAllDepartureActive()
        {
            return requestRepository?.GetAllDepartureActive();
        }

        public Request GetById(string id)
        {
            var requestId = Guid.Parse(id);
            return requestRepository?.GetById(requestId);
        }

        public void DeclineConnect(string id, Supervisor supervisor)
        {
            var requestId = Guid.Parse(id);
            var requestToDecline = requestRepository?.GetConnectById(requestId);
            requestToDecline.SetStatus(RequestStatus.Declined);
            requestToDecline.SetSupervisor(supervisor);
            requestRepository?.Update(requestToDecline);
            persistenceContext.SaveChanges();
        }

        public void DeclineDeparture(string id, Supervisor supervisor)
        {
            var requestId = Guid.Parse(id);
            var requestToDecline = requestRepository?.GetDepartureById(requestId);
            requestToDecline.SetStatus(RequestStatus.Declined);
            requestToDecline.SetSupervisor(supervisor);
            requestRepository?.UpdateDeparture(requestToDecline);
            persistenceContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAllSenders(IEnumerable<Request> requestsList)
        {
            var employeesList = new List<Employee>();

            foreach (var request in requestsList)
            {
                var driverDb = driverRepository.GetById(request.SenderId);

                if (driverDb != null)
                {
                    employeesList.Add(driverDb);
                }
                else
                {
                    var dispatcherDb = dispatcherRepository?.GetById(request.SenderId);
                    employeesList.Add(dispatcherDb);
                }
            }

            return employeesList;
        }

        public void AcceptConnect(string id, Supervisor supervisor)
        {
            var requestId = Guid.Parse(id);
            var requestToAccept = requestRepository?.GetConnectById(requestId);
            requestToAccept.SetStatus(RequestStatus.Accepted);
            requestToAccept.SetSupervisor(supervisor);
            requestRepository?.Update(requestToAccept);
            persistenceContext.SaveChanges();
        }

        public void AcceptDeparture(string id, Supervisor supervisor)
        {
            var requestId = Guid.Parse(id);
            var requestToAccept = requestRepository?.GetDepartureById(requestId);
            requestToAccept.SetStatus(RequestStatus.Accepted);
            requestToAccept.SetSupervisor(supervisor);
            requestRepository?.UpdateDeparture(requestToAccept);
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

        public IEnumerable<Request> GetConnectHistory()
        {
            return requestRepository?.GetConnectHistory();
        }

        public IEnumerable<DepartureRequest> GetDepartureHistory()
        {
            return requestRepository?.GetDepartureHistory();
        }
    }
}
