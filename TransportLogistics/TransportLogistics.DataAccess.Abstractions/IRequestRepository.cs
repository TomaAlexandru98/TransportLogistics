using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IRequestRepository : IBaseRepository<Request>
    {
        IEnumerable<Request> GetAllActive();
        IEnumerable<Request> FilterByTrailerId(Guid requestId);
    }
}
