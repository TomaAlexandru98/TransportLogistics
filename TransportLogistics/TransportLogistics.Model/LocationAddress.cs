using System;
using System.Collections.Generic;
using System.Text;


namespace TransportLogistics.Model
{
    public class LocationAddress : DataEntity
    {
        public string Country { get; protected set; }
        public string City { get; protected set; }
        public string Street { get; protected set; }
        public int StreetNumber { get; protected set; }
        public string PostalCode { get; protected set; }
    }
}
