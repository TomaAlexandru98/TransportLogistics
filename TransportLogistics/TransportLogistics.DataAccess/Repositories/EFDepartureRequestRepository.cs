using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFDepartureRequestRepository : EFBaseRepository<DepartureRequest>, IDepartureRequestRepository
    {
        public EFDepartureRequestRepository(TransportLogisticsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
