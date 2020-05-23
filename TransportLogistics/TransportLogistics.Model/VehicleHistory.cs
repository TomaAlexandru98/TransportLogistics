using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class VehicleHistory : DataEntity
    {
        public ICollection<VehicleDriver> VehicleDriver { get; private set; }

        public static VehicleHistory Create()
        {
            return new VehicleHistory
            {
                Id = Guid.NewGuid(),
                VehicleDriver = new List<VehicleDriver>()
            };
        }
    }
}
