using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Request:DataEntity
    {
        public Driver Driver { get; private set; }
        public Trailer Trailer { get; private set; }
        public Supervisor Supervisor { get; private set; }
    }
}
