using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Data.Abstractions
{
    public interface IVehiclesRepository
    {
        IEnumerable<VehicleData> GetAll();
    }
}
