using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Data;

namespace TransportLogistics.ApplicationLogic.Models
{
    public class Vehicle : VehicleData
    {
        private Vehicle() 
        {

        }
        public static Vehicle Create(string Name,string Type,int MaximCarryWeight,string VIN)
        {
            if (Name == null || Type == null || VIN == null)
            {
                throw new ArgumentNullException();
            }
            if(MaximCarryWeight < 0)
            {
                throw new Exception("Maxim carry weight can not be less than 0");
            }
            return new Vehicle
            {
                Name = Name,
                Type = Type,
                MaximCarryWeightKg = MaximCarryWeight,
                VIN = VIN,
                Id = Guid.NewGuid(),
                CurrentTrailers = new List<TrailerData>()
            };
        }
       
        
    }
}
