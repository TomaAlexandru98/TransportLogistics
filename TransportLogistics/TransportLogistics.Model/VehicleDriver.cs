using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class VehicleDriver : DataEntity
    {
        public Vehicle Vehicle { get; set; }
        public Driver Driver { get; set; }
    }
}
