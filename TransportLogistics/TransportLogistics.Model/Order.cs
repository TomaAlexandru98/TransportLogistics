using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public enum Status { Created, Assigned, PickedUp, Delivering, Delivered };
    public class Order : DataEntity
    {
        public Tuple<LocationAddress, LocationAddress> Route { get; protected set; }
        public Customer Recipient { get; protected set; }
    }
}
