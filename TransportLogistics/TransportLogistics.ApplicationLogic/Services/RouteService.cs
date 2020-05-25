using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
    public class RouteService
    {
        private readonly IPersistenceContext persistenceContext;
        //private readonly IOrderRepository orderRepository;
        private readonly IRouteRepository routeRepository;
            
        public RouteService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
            //orderRepository = persistenceContext.OrderRepository;
            routeRepository = persistenceContext.RouteRepository;
        }

        public Route CreateRoute(Vehicle vehicle)
        {
            var route = Route.Create(vehicle);
            routeRepository.Add(route);
            persistenceContext.SaveChanges();
            return route;
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
            return routeRepository.GetById(guid);
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
