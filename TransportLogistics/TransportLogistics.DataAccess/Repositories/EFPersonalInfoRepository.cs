using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFPersonalInfoRepository:EFBaseRepository<PersonalInfoRequest> , IPersonalInfoRepository
    {
        public EFPersonalInfoRepository(TransportLogisticsDbContext context) : base(context)
        {

        }

        public IEnumerable<PersonalInfoRequest> GetAllCreatedRequests()
        {
            var requests = dbContext.EditPersonalInfoRequests.Where(o => o.Status == EditStatusRequest.Created);
            return requests;
        }
    }
}
