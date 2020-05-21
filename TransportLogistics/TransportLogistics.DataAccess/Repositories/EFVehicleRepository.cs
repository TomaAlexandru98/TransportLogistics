using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;
using TransportLogistics.DataAccess.Abstractions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFVehicleRepository : EFBaseRepository<Vehicle>, IVehicleRepository
    {
        public EFVehicleRepository(TransportLogisticsDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<RouteEntry> GetDetailsRoute(Guid vehicleId, Guid routeId)
        {
            var vehicleHistory = GetHistory(vehicleId);
            
            foreach (var vehicle in vehicleHistory)
            {
                foreach(var route in vehicle.Driver.RoutesHistoric.Routes)
                {
                    if (route.Id == routeId)
                    {
                        return route.RouteEntries;
                    }
                }
            }

            return new List<RouteEntry>();
        }

        public IEnumerable<VehicleDriver> GetHistory(Guid id)
        {
            var vdList = dbContext.VehicleDrivers
                            .Where(vehicleDriver => vehicleDriver.Vehicle.Id == id)
                            .Include(vehicleDriver => vehicleDriver.Driver)
                            .ThenInclude(vehicleDriver => vehicleDriver.RoutesHistoric)
                            .ThenInclude(vehicleDriver => vehicleDriver.Routes);


            foreach (var vehicleDriver in vdList)
            {
                foreach(var route in vehicleDriver.Driver.RoutesHistoric.Routes)
                {
                    var routeDb = dbContext.Routes.Where(r => r.Id == route.Id)
                                                  .Include(r => r.RouteEntries)
                                                  .SingleOrDefault();

                    foreach (var routeEntry in routeDb.RouteEntries)
                    {
                        var routeEntryDb = dbContext.RouteEntries.Where(re => re.Id == routeEntry.Id)
                                                                 .Include(re => re.Order)
                                                                 .ThenInclude(re => re.DeliveryAddress)
                                                                 .Include(re => re.Order)
                                                                 .ThenInclude(re => re.PickUpAddress)
                                                                 .SingleOrDefault();
                        route.RouteEntries.Add(routeEntry);
                    }
                }

            }


            return vdList;
        }
    }
}
