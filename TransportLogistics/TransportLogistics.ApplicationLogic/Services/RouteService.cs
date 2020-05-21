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
        private readonly IOrderRepository orderRepository;
        private readonly IRouteRepository routeRepository;

        public RouteService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
            orderRepository = persistenceContext.OrderRepository;
            routeRepository = persistenceContext.RouteRepository;
        }

        public Route CreateRoute()
        {
            var route = Route.Create();
            routeRepository.Add(route);
            persistenceContext.SaveChanges();
            return route;
        }
        public Route AddEntry(string routeId, RouteEntry entry)
        {
            Guid.TryParse(routeId, out Guid routepGuid);
            var route = routeRepository.GetById(routepGuid);
            route.RouteEntries.Add(entry);
            return route;
        }
        public IEnumerable<Route> GetAllRoutes()
        {
            return routeRepository.GetAll();
        }
    }
}