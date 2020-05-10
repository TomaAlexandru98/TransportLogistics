using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;

namespace TransportLogistics.ApplicationLogic.Services
{
   public class EmployeeServices
    {
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IPersistenceContext PersistenceContext;
        public EmployeeServices(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            EmployeeRepository = persistenceContext.EmployeeRepository;
        }
        public void AddEmployee(string userId, string name, string email, string role)
        {
            EmployeeRepository.AddEmployee(userId, name, email, role);
        }
    }
}
