using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
   public class EmployeeServices
    {
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IPersistenceContext PersistenceContext;
        private readonly IDriverRepository DriverRepository;
       // private readonly IDispatcherRepository DispatcherRepository;
       //private readonly ISupervisorRepository SupervisorRepository;
        public EmployeeServices(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            EmployeeRepository = persistenceContext.EmployeeRepository;
            DriverRepository = persistenceContext.DriverRepository;
            //DispatcherRepository = persistenceContext.DispatcherRepository
            //SupervisorRepository = persistenceContext.SupervisorRepository
        }
        public void AddEmployee(string userId, string name, string email, string role)
        {
            EmployeeRepository.AddEmployee(userId, name, email, role);
        }
        public void DeleteEmployee(string userId)
        {
            var employee = GetEmployee(userId);
            DriverRepository.Remove(employee.Id);
            //SupervisorRepository.Remove(id);
            //DispatcherRepository.Remove(id);
        }
        public Employee GetEmployee(string userId)
        {
            Employee employee = new Employee();
            employee = DriverRepository.GetByUserId(userId);
            if (employee == null)
            {
                //employee = SupervisorRepository.GetByUserId(userId);
            }
            if (employee == null)
            {
                //employee = DispatcherRepository.GetByUserId(userId);
            }
            return employee;
        }
    }
}
