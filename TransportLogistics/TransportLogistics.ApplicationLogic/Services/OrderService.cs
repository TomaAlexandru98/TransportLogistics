using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
   public class OrderService
    {
        private readonly IPersistenceContext PersistenceContext;
        private readonly IOrderRepository OrderRepository;
        private readonly ICustomerRepository customerRepository;
        public OrderService(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            OrderRepository = persistenceContext.OrderRepository;
            customerRepository = persistenceContext.CustomerRepository;
        }
        public void ChangeOrderStatus(Guid orderId, string status)
        {
            OrderStatus enumStatus = OrderStatus.Assigned;

            switch (status)
            {
                case "PickedUp":
                    enumStatus = OrderStatus.PickedUp;
                    break;
                case "Delivered":
                    enumStatus = OrderStatus.Delivered;
                    break;
            }

            var Order = OrderRepository.GetById(orderId);
            Order.SetStatus(enumStatus);
            OrderRepository.Update(Order);
            PersistenceContext.SaveChanges();
        }

        //when a driver starts up a route,all the order with status PickedUp should be changed into Delivering
        public void StartRoute(ICollection<RouteEntry> routeEntries)
        {
            foreach(var routeEntry in routeEntries)
            {
                if(routeEntry.Order.Status == OrderStatus.PickedUp)
                {
                    routeEntry.Order.SetStatus(OrderStatus.Delivering);
                }
            }
        }

        public Order CreateOrder(Customer recipient, string pickupId, string deliveryId, decimal price)
        {
            Guid.TryParse(pickupId, out Guid pickupGuid);
            var pickupLocation = customerRepository.GetLocationAddress(pickupGuid);

            Guid.TryParse(deliveryId, out Guid deliveryGuid);
            var deliveryLocation = customerRepository.GetLocationAddress(deliveryGuid);

            var order = Order.Create(recipient, pickupLocation, deliveryLocation, price);
            
            OrderRepository.Add(order);
            PersistenceContext.SaveChanges();
            return order;
        }

        public Order GetById(string Id)
        {
            Guid.TryParse(Id, out Guid guid);
            return OrderRepository.GetById(guid);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return OrderRepository.GetAll();
        }

        public bool Remove(string id)
        {
            Guid orderId = Guid.Empty;
            Guid.TryParse(id, out orderId);

            var result = OrderRepository?.Remove(orderId);
            if (result == true)
            {
                PersistenceContext.SaveChanges();
                return true;
            }

            return false;
        }
        public Customer GetRecipient(string id)
        {
            return null;
        }
        public Order Update(string id,string pickupId,string deliveryId, decimal price)
        {
            Guid.TryParse(pickupId, out Guid pickupGuid);
            var pickupLocation = customerRepository.GetLocationAddress(pickupGuid);

            Guid.TryParse(deliveryId, out Guid deliveryGuid);
            var deliveryLocation = customerRepository.GetLocationAddress(deliveryGuid);

            var order = GetById(id);
            order.Update(pickupLocation, deliveryLocation, price);
            PersistenceContext.SaveChanges();
            return order;
        }
    }
}
