using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public enum RequestStatus { Accepted, Declined, Active }
    public class Request : DataEntity
    {
        public Guid SenderId { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public Trailer Trailer { get; private set; }
        public Supervisor Supervisor { get; private set; }
        public RequestStatus Status { get; private set; }
        public DateTime Date { get; private set; }

        public static Request Create(Guid senderId,
                                     Vehicle vehicle,
                                     Trailer trailer)
        {
            return new Request
            {
                Id = Guid.NewGuid(),
                SenderId = senderId,
                Vehicle = vehicle,
                Trailer = trailer,
                Status = RequestStatus.Active,
                Date = DateTime.UtcNow
            };
        }

        public RequestStatus ChangeStatus(RequestStatus status)
        {
            this.Status = status;
            return this.Status;
        }

        public Supervisor SetSupervisor(Supervisor supervisor)
        {
            this.Supervisor = supervisor;
            return this.Supervisor;
        }

        public void SetStatus(RequestStatus status)
        {
            Status = status;
        }
        public void SetSender(Guid senderId)
        {
            SenderId = senderId;
        }
        public void SetTrailer(Trailer trailer)
        {
            Trailer = trailer;
        }

        public void SetVehicle(Vehicle vehicle)
        {
            this.Vehicle = vehicle;
        }
    }
}
