using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public enum OrderType { Both,PickUp,Delivery}
    public class RouteEntry:DataEntity
    {
        public Order Order { get; private set; }
        public OrderType OrderType { get; private set; }
        public void SetOrder(Order order)
        {
            Order = order;
        }

        public void SetType(OrderType type)
        {
            OrderType = type;
        }
    }
}
