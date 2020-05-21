using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IDriverRepository:IBaseRepository<Driver>
    {
        Driver GetByUserId(string userId);
        ICollection<RouteEntry> GetRouteEntries(Guid id);
        Driver GetDriverWithRoute(Guid id);
        new IEnumerable<Driver> GetAll();
        RoutesHistory GetRoutesHistory(Guid id);
        
    }
}
