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

        public OrderService(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            OrderRepository = persistenceContext.OrderRepository;
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
            var Order =OrderRepository.GetById(orderId);
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

        public Order CreateOrder(Customer customer, string RecipientId, decimal price)
        {
            var order = Order.Create(customer, RecipientId, price);
            OrderRepository.Add(order);
            PersistenceContext.SaveChanges();
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return OrderRepository.GetAll();
        }
    }
}
