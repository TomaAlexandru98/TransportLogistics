using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.Data.Abstractions;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
   public class DriverService
    {
        private readonly IDriverRepository DriverRepository;
        private readonly IPersistenceContext PersistenceContext;
        private readonly IRouteRepository RouteRepository;
        private readonly IRequestRepository RequestRepository;
        private readonly ITrailerRepository TrailerRepository;
        private readonly OrderService OrderService;
        public DriverService(IPersistenceContext persistenceContext, OrderService orderService )
        {
            PersistenceContext = persistenceContext;
            DriverRepository = persistenceContext.DriverRepository;
            RouteRepository = persistenceContext.RouteRepository;
            RequestRepository = persistenceContext.RequestRepository;
            TrailerRepository = persistenceContext.TrailerRepository;
            OrderService = orderService;
        }
        public Driver GetByUserId(string userId)
        {
            return DriverRepository.GetByUserId(userId);
        }
        public ICollection<RouteEntry> GetRouteEntries(Guid id)
        {
          return DriverRepository.GetRouteEntries(id);
          
        }

        public void EndCurrentRoute(Driver driver)
        {
            driver = DriverRepository.GetDriverWithRoute(driver.Id);
            driver.AddRouteToHistoric(driver.CurrentRoute);
            driver.CurrentRoute.SetFinishTime();
            driver.SetCurrentRouteNull();
            SetDriverStatus(driver, DriverStatus.Free);
            DriverRepository.Update(driver);
        }
        public void SetDriverStatus(Driver driver,DriverStatus status)
        {
            driver.SetStatus(status);
            var routeEntries = GetRouteEntries(driver.Id);
            OrderService.StartRoute(routeEntries);
            if(status == DriverStatus.Driving)
            {
                driver.CurrentRoute.SetStartTime();
            }
            
            DriverRepository.Update(driver);
            PersistenceContext.SaveChanges();
        }

        public IEnumerable<Driver> GetAllDrivers()
        {
            return DriverRepository.GetAll();
        }

        public RoutesHistory GetRoutesHistory(Guid id)
        {
            return DriverRepository.GetRoutesHistory(id);
        }
        public Route GetRouteById(Guid id)
        {

            return RouteRepository.GetRouteById(id);
        }
        public void CreateRequest(Guid driverId , string registrationNumber)
        {
            Request request = new Request()
            {
                Id = Guid.NewGuid(),
               
            };
            var driver = DriverRepository.GetRouteWithVehicle(driverId);
            var trailer = TrailerRepository.GetByRegistrationNumber(registrationNumber);
            request.SetTrailer(trailer);
            request.SetVehicle(driver.CurrentRoute.Vehicle);
            request.SetStatus(RequestStatus.Active);
            request.SetSender(driver.Id);
            RequestRepository.Add(request);
            PersistenceContext.SaveChanges();
            
        }

    }
}
