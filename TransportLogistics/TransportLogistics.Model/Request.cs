using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public enum RequestStatus { Accepted, Declined, Holding }
    public class Request:DataEntity
    {
        public Driver Driver { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public Trailer Trailer { get; private set; }
        public Supervisor Supervisor { get; private set; }
        public RequestStatus Status { get; private set; }
    }
}
