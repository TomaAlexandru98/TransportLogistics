using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Employee : DataEntity
    {
        public string UserId { get; protected set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
       
    }
}
