using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IRequestRepository : IBaseRepository<Request>
    {
        IEnumerable<Request> GetAllConnectActive();
        IEnumerable<DepartureRequest> GetAllDepartureActive();
        IEnumerable<Request> FilterByTrailerId(Guid trailerId);
        Request GetConnectById(Guid id);
        DepartureRequest GetDepartureById(Guid id);
        DepartureRequest UpdateDeparture(DepartureRequest requestToDecline);
        IEnumerable<Request> GetConnectHistory();
        IEnumerable<DepartureRequest> GetDepartureHistory();
        DepartureRequest AddDeparture(DepartureRequest departureRequest);
    }
}
