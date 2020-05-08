using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Supervisor : Employee
    {
        public static Supervisor Create(string userId , string name,string email)
        {
            Supervisor supervisor = new Supervisor
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserId = userId,
                Name = name
                
            };
        return supervisor;
        }
    }
}
