using System;

namespace TransportLogistics.Model
{
    public enum OrderStatus { Created, Assigned, PickedUp, Delivering, Delivered, Canceled };

    public class Order : DataEntity
    {
        public LocationAddress PickUpAddress { get; private set; }
        public LocationAddress DeliveryAddress { get; private set; }
        public Recipient Recipient { get; private set; }
        public Customer Sender { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime PickUpTime { get; private set; }
        public DateTime DeliveryTime { get; private set; }

        public void SetStatus(OrderStatus status)
        {
            Status = status;
        }

        public static Order Create(Recipient recipient, Customer sender, LocationAddress pickup, 
            LocationAddress delivery, decimal price)

        {
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                DeliveryAddress = delivery,
                PickUpAddress = pickup,
                Price = price,
                Recipient = recipient,
                Sender = sender,
                Status = OrderStatus.Created,
                CreationTime = DateTime.UtcNow
            };
            return order;
        }

        public Order Update(LocationAddress pickup, LocationAddress delivery, decimal price)
        {
            PickUpAddress = pickup;
            DeliveryAddress = delivery;
            Price = price;

            return this;
        }

        public void SetPickUpTime()
        {
            PickUpTime = DateTime.UtcNow;
        }

        public void SetDeliveryTime()
        {
            DeliveryTime = DateTime.UtcNow;
        }
    }
}