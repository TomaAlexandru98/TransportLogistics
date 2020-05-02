using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Data
{
    public class VehicleData : DataEntity
    {
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public string RegistrationNumber { get; protected set; }
        public ICollection<TrailerData> CurrentTrailers { get; protected set; }
        public int MaximCarryWeightKg { get; protected set; }
        public string VIN { get; protected set; }
        
        protected VehicleData()
        {

        }
    }
}
