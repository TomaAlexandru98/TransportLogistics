using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public enum RequestStatus { Accepted, Declined, Holding }
    public class Request : DataEntity
    {
        public Driver Driver { get; private set; }
        public Trailer Trailer { get; private set; }
        public Supervisor Supervisor { get; private set; }
        public RequestStatus Status { get; private set; }

        public RequestStatus ChangeStatus(RequestStatus status)
        {
            this.Status = status;
            return this.Status;
        }

        public Supervisor AddSupervisor(Supervisor supervisor)
        {
            this.Supervisor = supervisor;
            return this.Supervisor;
        }
    }
}
