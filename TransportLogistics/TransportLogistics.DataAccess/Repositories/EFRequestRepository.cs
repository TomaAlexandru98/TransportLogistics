using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public override Request GetById(Guid id)
        {
            return dbContext.Requests.Where(request => request.Id == id)
                .Include(request => request.Vehicle)
                .Include(request => request.Trailer)
                .Include(request => request.Supervisor)
                .SingleOrDefault();
        }

        public IEnumerable<Request> FilterByVehicleId(Guid vehicleId)
        {
            return dbContext.Requests.Where(request => request.Vehicle.Id == vehicleId)
                .Include(request => request.Vehicle)
                .Include(request => request.Trailer)
                .Include(request => request.Supervisor);
        }

        public override IEnumerable<Request> GetAll()
        {
            return dbContext.Requests
                .Include(request => request.Vehicle)
                .Include(request => request.Trailer)
                .Include(request => request.Supervisor);
        }

        public IEnumerable<Request> GetAllActive()
        {
            return dbContext.Requests.Where(request => request.Status == RequestStatus.Active)
                .Include(request => request.Vehicle)
                .Include(request => request.Trailer)
                .Include(request => request.Supervisor);
        }
    }
}
