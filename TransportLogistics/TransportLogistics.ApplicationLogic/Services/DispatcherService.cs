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
        private readonly IDepartureRequestRepository departureRequestRepository;

        public DispatcherService(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            DispatcherRepository = persistenceContext.DispatcherRepository;
            trailerRepository = persistenceContext.TrailerRepository;
            driverRepository = persistenceContext.DriverRepository;
            vehicleRepository = persistenceContext.VehicleRepository;
            routeRepository = persistenceContext.RouteRepository;
            departureRequestRepository = persistenceContext.DepartureRequestRepository;
        }
        public Dispatcher GetByUserId(string userId)
        {
            return DispatcherRepository.GetByUserId(userId);
        }

        public void ConnectDriverToRoute(Route route, Driver driver, Dispatcher dispatcher)
        {
            driver.SetCurrentRoute(route);
            route.SetStatus(RouteStatus.Assigned);

            var departureRequest = DepartureRequest.Create(dispatcher, driver);

            this.departureRequestRepository.Add(departureRequest);
            this.routeRepository.Update(route);
            this.driverRepository.Update(driver);
            this.PersistenceContext.SaveChanges();
        }
    }
}
