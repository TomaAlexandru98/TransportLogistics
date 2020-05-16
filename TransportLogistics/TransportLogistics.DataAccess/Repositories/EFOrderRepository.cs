using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;
using TransportLogistics.Data.Abstractions;
using TransportLogistics.DataAccess.Abstractions;

namespace TransportLogistics.DataAccess.Repositories
{
    class EFOrderRepository : EFBaseRepository<Order>, IOrderRepository
    {
        public EFOrderRepository(TransportLogisticsDbContext context) : base(context)
        {

        }
       
    }
}
