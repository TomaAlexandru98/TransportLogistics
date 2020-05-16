using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public enum OrderStatus { Created, Assigned, PickedUp, Delivering, Delivered };
   
    public class Order : DataEntity
    {
        public LocationAddress PickUpAddress { get; private set; }
        public LocationAddress DeliveryAddress { get; private set; }
        public Customer Recipient { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal Price { get; private set; }
      
        public void SetStatus(OrderStatus status)
        {
            Status = status;
        }
        
      
    }
}
