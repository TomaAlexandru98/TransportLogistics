using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class VehicleChangeRequest:DataEntity
    {
        public Vehicle OldVehicle { get; private set; }
        public Vehicle NewVehicle { get; private set; }
        public Driver Driver { get; private set; }
        public Dispatcher Dispatcher { get; private set; }

        //public VehicleChangeRequest Create(Vehicle oldVehicle,Vehicle newVehicle,Driver driver)
        //{
        //    var request = new VehicleChangeRequest()
        //    {
        //        Id = Guid.NewGuid(),
        //        OldVehicle = oldVehicle,
        //        NewVehicle = newVehicle,
        //        DriverId = driver
        //    };
        //    return request; 
        //}
    }
}
