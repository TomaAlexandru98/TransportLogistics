using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Data
{
    public class EmployeeData : DataEntity
    {
        public string Email { get; protected set; }
        public string Name { get; protected set; }
    }
}
