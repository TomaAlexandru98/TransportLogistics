using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Data
{
    public class ContactData : DataEntity
    {
        public string PhoneNo { get; protected set; }
        public string Email { get; protected set; }
    }
}
