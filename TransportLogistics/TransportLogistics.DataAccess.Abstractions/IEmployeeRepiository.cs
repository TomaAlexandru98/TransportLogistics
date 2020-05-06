using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IEmployeeRepiository
    {
        void AddEmployee(string userId, string name, string email,string role);
    }
}
