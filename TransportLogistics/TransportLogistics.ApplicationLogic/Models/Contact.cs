using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.ApplicationLogic.Models
{
    public class Contact : DataEntity
    {
        public string PhoneNo { get; private set; }
        public string Email { get; private set; }
    }
}
