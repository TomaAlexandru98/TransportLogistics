using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFRecipientRepository : EFBaseRepository<Recipient>, IRecipientRepository
    {
        public EFRecipientRepository(TransportLogisticsDbContext dbContext) : base(dbContext)
        { }
    


    }
}
