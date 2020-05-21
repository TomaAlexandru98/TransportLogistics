using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        IEnumerable<VehicleDriver> GetHistory(Guid id);
        IEnumerable<RouteEntry> GetDetailsRoute(Guid guidVehicleId1, Guid guidVehicleId2);
    }
}
