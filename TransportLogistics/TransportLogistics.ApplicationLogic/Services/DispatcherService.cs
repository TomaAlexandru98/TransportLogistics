using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Data.Abstractions;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
    public class DispatcherService
    {
       private readonly IDispatcherRepository DispatcherRepository;
        private readonly IPersistenceContext PersistenceContext;
        private readonly IDriverRepository driverRepository;
        private readonly ITrailerRepository trailerRepository;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IRouteRepository routeRepository;

        public DispatcherService(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            DispatcherRepository = persistenceContext.DispatcherRepository;
            trailerRepository = persistenceContext.TrailerRepository;
            driverRepository = persistenceContext.DriverRepository;
            vehicleRepository = persistenceContext.VehicleRepository;
            routeRepository = persistenceContext.RouteRepository;
        }
        public Dispatcher GetByUserId(string userId)
        {
            return DispatcherRepository.GetByUserId(userId);
        }

        public void ConnectDriverToRoute(string routeId, string driverId)
        {
            var guidRouteId = Guid.Parse(routeId);
            var RouteDb = this.routeRepository?.GetRouteById(guidRouteId);

            var guidDriverId = Guid.Parse(driverId);
            var driverDb = this.driverRepository?.GetById(guidDriverId);


            driverDb.SetCurrentRoute(RouteDb);
            RouteDb.SetStatus(RouteStatus.Assigned);
            this.routeRepository.Update(RouteDb);
            this.driverRepository.Update(driverDb);
            this.PersistenceContext.SaveChanges();
        }
    }
}
