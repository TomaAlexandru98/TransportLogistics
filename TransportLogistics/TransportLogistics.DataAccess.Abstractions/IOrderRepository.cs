using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IOrderRepository:IBaseRepository<Order>
    {
        //  void ChangeOrderStatus(Guid orderId,OrderStatus status);

        new IEnumerable<Order> GetAll();
    }
}
