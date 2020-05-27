using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
    public class DispatcherService
    {
       private readonly IDispatcherRepository DispatcherRepository;
        private readonly IPersistenceContext PersistenceContext;
       public DispatcherService(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            DispatcherRepository = persistenceContext.DispatcherRepository;
        }
        public Dispatcher GetByUserId(string userId)
        {
            return DispatcherRepository.GetByUserId(userId);
        }
    }
}
