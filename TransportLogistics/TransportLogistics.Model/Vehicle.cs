using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Vehicle : DataEntity
    {
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public string RegistrationNumber { get; protected set; }
        public ICollection<Trailer> CurrentTrailers { get; protected set; }
        public int MaximCarryWeightKg { get; protected set; }
        public string VIN { get; protected set; }
    }
}
