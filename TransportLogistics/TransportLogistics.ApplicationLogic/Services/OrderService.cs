﻿using System;
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
        public void ChangeOrderStatus(Guid orderId, OrderStatus status)
        {
            var Order =OrderRepository.GetById(orderId);
            Order.SetStatus(status);
            OrderRepository.Update(Order);
            PersistenceContext.SaveChanges();

        }

        public Order CreateOrder(LocationAddress deliveryAddress, LocationAddress pickUpAddress, Customer recipient, decimal price)
        {
            var order = Order.Create(deliveryAddress, pickUpAddress, recipient, price);
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
