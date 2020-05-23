using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFRequestRepository : EFBaseRepository<Request>, IRequestRepository
    {
        public EFRequestRepository(TransportLogisticsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
