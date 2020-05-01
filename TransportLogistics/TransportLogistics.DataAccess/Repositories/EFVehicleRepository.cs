using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Data;
using TransportLogistics.Data.Abstractions;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFVehicleRepository:EFBaseRepository<VehicleData>, IVehiclesRepository
    {
        public EFVehicleRepository(TransportLogisticsDbContext dbContext):base(dbContext)
        {
           
        }
    }
}
