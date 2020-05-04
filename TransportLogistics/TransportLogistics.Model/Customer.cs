using System;
using System.Collections.Generic;
using System.Text;


namespace TransportLogistics.Model
{
    public class Customer : DataEntity    {
        public ICollection<LocationAddress> LocationAddresses { get; protected set; }
        public string Name { get; protected set; }
        public Contact ContactDetails { get; protected set; }
    }

}
