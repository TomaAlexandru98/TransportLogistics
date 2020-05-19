using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
   public class DriverService
    {
        private readonly IDriverRepository DriverRepository;
        private readonly IPersistenceContext PersistenceContext;
        private readonly OrderService OrderService;
        public DriverService(IPersistenceContext persistenceContext, OrderService orderService)
        {
            PersistenceContext = persistenceContext;
            DriverRepository = persistenceContext.DriverRepository;
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
            driver.SetCurrentRouteNull();
            SetDriverStatus(driver, DriverStatus.Free);
            DriverRepository.Update(driver);
        }
        public void SetDriverStatus(Driver driver,DriverStatus status)
        {
            driver.SetStatus(status);
            var routeEntries = GetRouteEntries(driver.Id);
            OrderService.StartRoute(routeEntries);
            DriverRepository.Update(driver);
            PersistenceContext.SaveChanges();
        }
        
    }
}
