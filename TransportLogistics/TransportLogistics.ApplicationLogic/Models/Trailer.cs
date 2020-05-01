using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.ApplicationLogic.Models
{
    public class Trailer : DataEntity
    {
        public int MaximWeightKg { get; protected set; }
    }
}
