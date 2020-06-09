using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IRouteRepository : IBaseRepository<Route>
    {
        Route GetRouteById(Guid routeId);
        new IEnumerable<Route> GetAll();
        RouteEntry Add(RouteEntry entry,Guid RouteId);
        //Route Create();
        IEnumerable<RouteEntry> GetAllRouteEntries();

        RouteEntry GetEntry(Guid id);
        void Remove(RouteEntry entry, Guid RouteId);


    }
}
