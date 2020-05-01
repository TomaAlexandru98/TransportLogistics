using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Data
{
    public class CustomerData : DataEntity
    {
        public ICollection<LocationAddressData> LocationAddresses { get; protected set; }
        public string Name { get; protected set; }
        public ContactData ContactDetails { get; protected set; }
    }

}
