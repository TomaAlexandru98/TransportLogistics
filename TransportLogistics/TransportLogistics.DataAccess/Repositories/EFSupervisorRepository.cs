using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFSupervisorRepository:EFBaseRepository<Supervisor> , ISupervisorRepository
    {
        public EFSupervisorRepository(TransportLogisticsDbContext context):base(context)
        {

        }

        public Supervisor GetByUserId(string userId)
        {
            return dbContext.Supervisors.Where(o => o.UserId == userId).FirstOrDefault();
        }
    }
}
