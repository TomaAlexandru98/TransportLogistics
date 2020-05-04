using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.ApplicationLogic.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public Guid CustomerId { get; private set; }
        public CustomerNotFoundException(Guid customerId) : base($"Customer with id {customerId} was not found")
        {
            CustomerId = customerId;
        }
    }
}
