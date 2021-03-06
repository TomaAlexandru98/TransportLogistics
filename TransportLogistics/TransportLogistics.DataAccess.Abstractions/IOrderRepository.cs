﻿using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IOrderRepository:IBaseRepository<Order>
    {
        //  void ChangeOrderStatus(Guid orderId,OrderStatus status);

        new Order GetById(Guid orderId);

        new IEnumerable<Order> GetAll();
        bool RemoveOrder(Guid orderId);
        bool RemoveOrdersFromCustomer(Guid customerId);
    }
}
