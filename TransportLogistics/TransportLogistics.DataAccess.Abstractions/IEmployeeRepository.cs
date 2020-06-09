using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IEmployeeRepository
    {
        void AddEmployee(string userId, string name, string email,string role);
        
    }
}
