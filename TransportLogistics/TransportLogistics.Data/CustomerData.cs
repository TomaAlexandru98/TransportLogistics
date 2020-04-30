using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Data
{
    public class CustomerData : DataEntity
    {
        public List<LocationAddressData> LocationAddresses { get; private set; }
        public string Name { get; private set; }
        public ContactData ContactDetails { get; private set; }
    }

}
