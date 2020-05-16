using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Sevices
{
   public class DriverService
    {
        private readonly IDriverRepository DriverRepository;
        private readonly IPersistenceContext PersistenceContext;
        public DriverService(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            DriverRepository = persistenceContext.DriverRepository;
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
            DriverRepository.Update(driver);
        }
    }
}
