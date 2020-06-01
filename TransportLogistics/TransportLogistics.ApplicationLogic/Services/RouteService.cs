﻿using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
    public class RouteService
    {
        private readonly IPersistenceContext persistenceContext;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IRouteRepository routeRepository;
            
        public RouteService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
            vehicleRepository = persistenceContext.VehicleRepository;
            routeRepository = persistenceContext.RouteRepository;
        }

        public Route CreateRoute(Vehicle vehicle)
        {
            var route = Route.Create(vehicle);
            vehicle.UpdateStatus(VehicleStatus.Busy);
            route.SetVehicle(vehicle);
            routeRepository.Add(route);
            persistenceContext.SaveChanges();
            return route;
        }

        public void ChangeVehicle(Route route,Vehicle vehicle)
        {
            route.Vehicle.UpdateStatus(VehicleStatus.Free);
            route.SetVehicle(vehicle);
            route.Vehicle.UpdateStatus(VehicleStatus.Busy);
            routeRepository.Update(route);
        }
        public Route AddEntry(string routeId, RouteEntry entry)
        {
            Guid.TryParse(routeId, out Guid routepGuid);
            var route = routeRepository.GetRouteById(routepGuid);
            routeRepository.Add(entry,route.Id);
            
            route.SetRouteEntry(entry);
          //  route.RouteEntries.Add(entry);
            persistenceContext.SaveChanges();
            return route;
        }

        public Route RemoveEntry(string routeId, RouteEntry entry)
        {
            Guid.TryParse(routeId, out Guid routepGuid);
            var route = routeRepository.GetRouteById(routepGuid);
            routeRepository.Remove(entry, route.Id);

            route.DeleteRouteEntry(entry);
            //  route.RouteEntries.Add(entry);
            persistenceContext.SaveChanges();
            return route;
        }

        public IEnumerable<Route> GetAllRoutes()
        {
            return routeRepository.GetAll();
        }
        public Route GetById(string Id)
        {
            Guid.TryParse(Id, out Guid guid);
            return routeRepository.GetRouteById(guid);
        }
        public RouteEntry GetEntryById(string id)
        {
            Guid.TryParse(id, out Guid guid);
            return routeRepository.GetEntry(guid);
        }
        public bool Remove(string id)
        {
            Guid routeId = Guid.Empty;
            Guid.TryParse(id, out routeId);

            var result = routeRepository?.Remove(routeId);
            if (result == true)
            {
                persistenceContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool RemoveRoute(string id)
        {
            Guid routeId = Guid.Empty;
            Guid.TryParse(id, out routeId);
            var route = routeRepository.GetRouteById(routeId);
            
            route.Vehicle.UpdateStatus(VehicleStatus.Free);
            route.DeleteRouteEntries();
            var result = routeRepository?.Remove(routeId);
            if (result == true)
            {
                persistenceContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
