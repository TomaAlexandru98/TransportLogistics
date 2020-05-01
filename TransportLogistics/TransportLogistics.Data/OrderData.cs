using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Data
{
    public enum Status{ Created, Assigned, PickedUp, Delivering, Delivered };
    public class OrderData : DataEntity
    {
        public Tuple<LocationAddressData, LocationAddressData> Route { get; protected set; }
        public CustomerData Recipient { get; protected set; }
    }
}
