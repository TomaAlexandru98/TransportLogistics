using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    public class EFEmployeeRepository: IEmployeeRepository
    {
      public EFEmployeeRepository(TransportLogisticsDbContext dbContext)
        {
            DbContext = dbContext;
          
        }

        private readonly TransportLogisticsDbContext DbContext;

        public void AddEmployee(string userId, string name, string email , string role)
        {
            if(role == "Driver")
            {
               var driver = Driver.Create(userId, name, email);
                DbContext.Drivers.Add(driver);
            }
            else if(role == "Supervisor")
            {
                var supervisor = Supervisor.Create(userId, name, email);
                DbContext.Supervisors.Add(supervisor);
                
            }
            else if(role == "Dispatcher")
            {
                var dispatcher = Dispatcher.Create(userId, name, email);
                DbContext.Dispatchers.Add(dispatcher);
            }
            DbContext.SaveChanges();
        }
       
    }
}
