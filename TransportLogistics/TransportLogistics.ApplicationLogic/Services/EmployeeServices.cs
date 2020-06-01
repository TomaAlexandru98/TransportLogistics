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
        private readonly IDispatcherRepository DispatcherRepository;
       private readonly ISupervisorRepository SupervisorRepository;
        public EmployeeServices(IPersistenceContext persistenceContext)
        {
            PersistenceContext = persistenceContext;
            EmployeeRepository = persistenceContext.EmployeeRepository;
            DriverRepository = persistenceContext.DriverRepository;
            DispatcherRepository = persistenceContext.DispatcherRepository;
            SupervisorRepository = persistenceContext.SupervisorRepository;
        }
        public void AddEmployee(string userId, string name, string email, string role)
        {
            EmployeeRepository.AddEmployee(userId, name, email, role);
        }
        public void DeleteEmployee(string userId)
        {
            var employee = GetEmployee(userId);

            if (employee.Id != null && DriverRepository.Remove(employee.Id) == false)
            {
                if(SupervisorRepository.Remove(employee.Id) == false)
                {

                    DispatcherRepository.Remove(employee.Id);
                }
            }
            PersistenceContext.SaveChanges();

        }
        public Employee GetEmployee(string userId)
        {
            Employee employee = new Employee();
            employee = DriverRepository.GetByUserId(userId);
            if (employee == null)
            {
                employee = DispatcherRepository.GetByUserId(userId);
            }
            if (employee == null)
            {
                employee = SupervisorRepository.GetByUserId(userId);
            }
            return employee;
        }

        public void UpdateEmployee(string name, string email,  string Role , string UserId)
        {
            var employee = GetEmployee(UserId);
            
            if(Role == "Driver")
            {
                var driver = DriverRepository.GetById(employee.Id);
                driver.SetName(name);
                driver.SetEmail(email);
                DriverRepository.Update(driver);
            }
            else if(Role == "Supervisor")
            {
                var supervisor = SupervisorRepository.GetById(employee.Id);
                supervisor.SetEmail(email);
                supervisor.SetName(name);
                SupervisorRepository.Update(supervisor);
            }
            else if(Role == "Dispatcher")
            {
                var dispatcher = DispatcherRepository.GetById(employee.Id);
                dispatcher.SetName(name);
                dispatcher.SetEmail(email);
                DispatcherRepository.Update(dispatcher);
            }
        }
    }
}
