using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class DepartureRequest : DataEntity
    {
        public Dispatcher Dispatcher { get; private set; }
        public Driver Driver { get; private set; }
        public Supervisor Supervisor { get; private set; }
        public RequestStatus Status { get; private set; }
        public DateTime Date { get; private set; }


        private DepartureRequest()
        {
        }

        public static DepartureRequest Create(Dispatcher dispatcher, Driver driver)
        {
            return new DepartureRequest
            {
                Id = Guid.NewGuid(),
                Dispatcher = dispatcher,
                Driver = driver,
                Status = RequestStatus.Active,
                Date = DateTime.UtcNow
            };
        }

        public Supervisor SetSupervisor(Supervisor supervisor)
        {
            this.Supervisor = supervisor;
            return this.Supervisor;
        }

        public RequestStatus SetStatus(RequestStatus status)
        {
            this.Status = status;
            return this.Status;
        }
    }
}
