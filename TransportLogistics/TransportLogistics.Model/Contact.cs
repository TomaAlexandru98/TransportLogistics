using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


namespace TransportLogistics.Model
{
    public class Contact : DataEntity
    {
        public string PhoneNo { get; protected set; }
        public string Email { get; protected set; }

        public void Update(string phoneNo, string email)
        {
            PhoneNo = phoneNo;
            Email = email;
        }
    }
}
