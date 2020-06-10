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

        public IEnumerable<Request> FilterByTrailerId(Guid trailerId)
        {
            return dbContext.Requests.Where(request => request.Trailer.Id == trailerId)
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

        public IEnumerable<Request> GetAllConnectActive()
        {
            return dbContext.Requests.Where(request => request.Status == RequestStatus.Active)
                .Include(request => request.Vehicle)
                .Include(request => request.Trailer)
                .Include(request => request.Supervisor);
        }

        private IEnumerable<DepartureRequest> GetAllDeparture()
        {
            var requestsList = dbContext.DepartureRequests
                                        .Include(request => request.Dispatcher)
                                        .Include(request => request.Driver)
                                        .ThenInclude(request => request.CurrentRoute)
                                        .ThenInclude(request => request.RouteEntries)
                                        .Include(request => request.Supervisor).ToList();

            foreach (var request in requestsList)
            {
                foreach (var routeEntry in request.Driver.CurrentRoute.RouteEntries)
                {
                    var routeEntryDb = dbContext.RouteEntries.Where(re => re.Id == routeEntry.Id)
                                                             .Include(re => re.Order)
                                                             .ThenInclude(re => re.PickUpAddress)
                                                             .Include(re => re.Order)
                                                             .ThenInclude(re => re.DeliveryAddress)
                                                             .SingleOrDefault();
                }
            }

            return requestsList;
        }

        public IEnumerable<DepartureRequest> GetAllDepartureActive()
        {
            return GetAllDeparture().Where(request => request.Status == RequestStatus.Active);
        }

        public Request GetConnectById(Guid id)
        {
            return dbContext.Requests.Where(request => request.Id == id).SingleOrDefault();
        }

        public DepartureRequest GetDepartureById(Guid id)
        {
            return dbContext.DepartureRequests.Where(request => request.Id == id).SingleOrDefault();
        }

        public DepartureRequest UpdateDeparture(DepartureRequest requestToDecline)
        {
            dbContext.DepartureRequests.Update(requestToDecline);
            return requestToDecline;
        }

        public IEnumerable<Request> GetConnectHistory()
        {
            return dbContext.Requests.Where(request => request.Status != RequestStatus.Active)
                                     .Include(request => request.Supervisor)
                                     .Include(request => request.Vehicle)
                                     .Include(request => request.Trailer).ToList();
        }

        public IEnumerable<DepartureRequest> GetDepartureHistory()
        {
            return GetAllDeparture().Where(request => request.Status != RequestStatus.Active);
        }

        public DepartureRequest AddDeparture(DepartureRequest departureRequest)
        {
            dbContext.DepartureRequests.Add(departureRequest);
            return departureRequest;
        }
    }
}
