using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Data.Abstractions;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
    public class SupervisorService
    {
        private readonly IPersistenceContext persistenceContext;
        private readonly IRequestRepository requestRepository;
        private readonly ISupervisorRepository supervisorRepository;
        private readonly IDriverRepository driverRepository;
        private readonly ITrailerRepository trailerRepository;
        private readonly IVehicleRepository vehicleRepository;

        public SupervisorService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
            this.requestRepository = persistenceContext.RequestRepository;
            this.driverRepository = persistenceContext.DriverRepository;
            this.supervisorRepository = persistenceContext.SupervisorRepository;
            this.trailerRepository = persistenceContext.TrailerRepository;
            this.vehicleRepository = persistenceContext.VehicleRepository;
        }

        public void ConnectTrailerToVehicle(Guid id, string vehicleId, string trailerId)
        {
            var guidVehicleId = Guid.Parse(vehicleId);
            var vehicleDb = this.vehicleRepository?.GetById(guidVehicleId);

            var guidTraielrId = Guid.Parse(trailerId);
            var trailerDb = this.trailerRepository?.GetById(guidTraielrId);

            vehicleDb.SetTrailer(trailerDb);
            trailerDb.SetStatus(Status.Busy);
            this.vehicleRepository.Update(vehicleDb);
            this.persistenceContext.SaveChanges();
        }

        public Supervisor GetByUserId(string userId)
        {
            return this.supervisorRepository?.GetByUserId(userId);
        }

        public Request ResponseRequest(string supervisorId, string requestId, RequestStatus status)
        {
            var guidSupervisorId = Guid.Parse(supervisorId);
            var supervisorDb = this.supervisorRepository?.GetById(guidSupervisorId);

            var guidRequestId = Guid.Parse(requestId);
            var requestDb = this.requestRepository?.GetById(guidRequestId);

            requestDb.ChangeStatus(status);
            requestDb.SetSupervisor(supervisorDb);
            return this.requestRepository?.Update(requestDb);
        }

        public Request AcceptRequest(string supervisorId, string requestId)
        {
            return ResponseRequest(supervisorId, requestId, RequestStatus.Accepted);
        }

        public Request DeclineRequest(string supervisorId, string requestId)
        {
            return ResponseRequest(supervisorId, requestId, RequestStatus.Declined);
        }
    }
}
