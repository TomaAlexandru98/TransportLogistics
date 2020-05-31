using System;
using System.Collections.Generic;
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
    }
}
