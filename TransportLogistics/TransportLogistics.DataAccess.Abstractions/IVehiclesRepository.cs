using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    interface IVehiclesRepository
    {
        IEnumerable<Vehicle> GetAll();
    }
}
