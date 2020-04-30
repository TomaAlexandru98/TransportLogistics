using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Data
{
    public class LocationAddressData : DataEntity
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public int StreetNumber { get; private set; }
        public string PostalCode { get; private set; }

    }
}
