using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;
using TransportLogistics.DataAccess.Abstractions;
namespace TransportLogistics.DataAccess.Repositories
{
    public class EFVehicleRepository : EFBaseRepository<Vehicle>, IVehicleRepository
    {
        public EFVehicleRepository(TransportLogisticsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
