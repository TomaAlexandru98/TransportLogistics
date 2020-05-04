using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class Vehicle : DataEntity
    {
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public string RegistrationNumber { get; protected set; }
        public ICollection<Trailer> CurrentTrailers { get; protected set; }
        public int MaximCarryWeightKg { get; protected set; }
        public string VIN { get; protected set; }
        public static Vehicle Create(string Name, string Type, int MaximCarryWeight, string VIN)
        {
            if (Name == null || Type == null || VIN == null)
            {
                throw new ArgumentNullException();
            }
            if (MaximCarryWeight < 0)
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
               // CurrentTrailers = new List<TrailerData>()
            };
        }
    }
}
