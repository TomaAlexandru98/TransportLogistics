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
        public ICollection<Order> GetOrders(Guid id)
        {
            return DriverRepository.GetOrders(id);
        }

        public void AddNewRoute(Guid driverId, ICollection<Order> orders)
        {
            var driver = DriverRepository.GetById(driverId);
            Route route =  Route.Create();
            foreach(var order in orders)
            {
                route.LocationAddresses.Add(order.PickUpAddress);
                route.LocationAddresses.Add(order.DeliveryAddress);
            }
            driver.RoutesHistory.Add(route);
            DriverRepository.Update(driver);
        }

        public ICollection<Order> GetFilteredOrders(Guid id)
        {
            
            var filteredOrders = GetOrders(id);
            if(filteredOrders != null)
            foreach (var order in filteredOrders)
            {
                if(order.Status == OrderStatus.Delivered)
                {
                    filteredOrders.Remove(order);
                }
            }
            return filteredOrders;
        }
    }
}
