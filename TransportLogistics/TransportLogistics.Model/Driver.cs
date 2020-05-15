using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public enum DriverStatus {Busy,Free }
    public class Driver: Employee
    {
        //public ICollection<Vehicle> VehicleHistory { get; protected set; }
        //replace Vehicle with another model which beside a vehicle also contains a span of time when that car was used by driver
        //public ICollection<Tuple<LocationAddressData, LocationAddressData>> RoutesHistory { get; protected set; }
       // public ICollection<Order> Orders { get; private set; }
        public RoutesHistory RoutesHistory { get; private set; }
        public Route CurrentRoute { get; private set; }
        public DriverStatus Status { get; private set; }
        public static Driver Create(string userId, string name, string email)
        {
            Driver driver = new Driver
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserId = userId,
                Name = name

            };
            return driver;
        }
    }
}
