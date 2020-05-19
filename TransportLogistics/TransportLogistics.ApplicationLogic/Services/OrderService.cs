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
        public void ChangeOrderStatus(Guid orderId, OrderStatus status)
        {
            var Order =OrderRepository.GetById(orderId);
            Order.SetStatus(status);
            OrderRepository.Update(Order);
            PersistenceContext.SaveChanges();

        }

        public Order CreateOrder(string RecipientId,LocationAddress pickup, LocationAddress delivery, decimal price)
        {
            Guid customerId = Guid.Parse(RecipientId);
            var recipient = customerRepository.GetById(customerId);
            var order = Order.Create(recipient, pickup, delivery, price);
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
