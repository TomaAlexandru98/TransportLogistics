using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Data
{
    public class VehicleData : DataEntity
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string RegistrationNumber { get; private set; }
        public List<TrailerData> Trailers { get; private set; }
        public int MaximCarryWeightKg { get; private set; }
    }
}
