using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IVehiclesRepository:IBaseRepository<Vehicle>
    {
       // IEnumerable<Vehicle> GetAll();
    }
}
