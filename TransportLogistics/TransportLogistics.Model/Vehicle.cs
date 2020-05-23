using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public enum VehicleStatus { Free, Busy, UnAvailable}
    public class Vehicle : DataEntity
    {
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public string RegistrationNumber { get; protected set; }
        public int MaximCarryWeightKg { get; protected set; }
        public string VIN { get; protected set; }
        public Trailer CurrentTrailer { get; protected set; }
        public VehicleHistory History { get; private set; }
        public VehicleStatus Status { get; protected set; }

        public static Vehicle Create(string Name, string Type, string registrationNumber, int MaximCarryWeight, string VIN)
        {
            if (MaximCarryWeight < 0)
            {
                throw new Exception("Maxim carry weight can not be less than 0");
            }

            return new Vehicle
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Type = Type,
                RegistrationNumber = registrationNumber,
                MaximCarryWeightKg = MaximCarryWeight,
                VIN = VIN,
                History = VehicleHistory.Create(),
                Status = VehicleStatus.Free
            };
        }

        public Vehicle Update(string name, string type, string registrationNumber, int maximCarryWeight, string vin)
        {
            this.Name = name;
            this.Type = type;
            this.RegistrationNumber = registrationNumber;
            this.MaximCarryWeightKg = maximCarryWeight;
            this.VIN = vin;

            return this;
        }

        public VehicleStatus UpdateStatus(VehicleStatus status)
        {
            this.Status = status;
            return this.Status;
        }
    }
}
