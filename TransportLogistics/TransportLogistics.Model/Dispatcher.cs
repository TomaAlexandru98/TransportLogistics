using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Dispatcher: Employee
    {
        public static Dispatcher Create(string userId, string name, string email)
        {
            Dispatcher dispatcher = new Dispatcher
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserId = userId,
                Name = name

            };
            return dispatcher;
        }
    }
}
