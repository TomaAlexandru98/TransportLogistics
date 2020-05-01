using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.ApplicationLogic.Models
{
    public class Customer : DataEntity
    {
        public ICollection<LocationAddress> LocationAddresses { get; private set; }
        public string Name { get; protected set; }
        public Contact ContactDetails { get; protected set; }
    }

}
