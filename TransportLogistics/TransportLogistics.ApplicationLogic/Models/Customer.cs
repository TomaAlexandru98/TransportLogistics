using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.ApplicationLogic.Models
{
    public class Customer : DataEntity
    {
        public List<LocationAddress> LocationAddresses { get; private set; }
        public string Name { get; private set; }
        public Contact ContactDetails { get; private set; }
    }

}
