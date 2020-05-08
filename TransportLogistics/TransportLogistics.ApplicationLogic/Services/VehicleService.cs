using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.ApplicationLogic.Services
{
    public class VehicleService
    {
        private readonly IPersistenceContext persistenceContext;
        private readonly IVehiclesRepository vehiclesRepository;

        public VehicleService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
            this.vehiclesRepository = persistenceContext.VehiclesRepository;
        }

        public Vehicle GetById(string id)
        {
            Guid vehicleId = Guid.Empty;
            Guid.TryParse(id, out vehicleId);

            return vehiclesRepository?.GetById(vehicleId);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return vehiclesRepository?.GetAll()
                                      .AsEnumerable();
        }

        public Vehicle Add(string name,
                           string type,
                           string registrationNumber,
                           int maximCarryWeight,
                           string vin)
        {
            var vehicleToAdd = Vehicle.Create(name, type, registrationNumber, maximCarryWeight, vin);
            vehiclesRepository?.Add(vehicleToAdd);
            persistenceContext?.SaveChanges();
            return vehicleToAdd;
        }

        public bool Remove(string id)
        {
            Guid vehicleId = Guid.Empty;
            Guid.TryParse(id, out vehicleId);

            var result = vehiclesRepository?.Remove(vehicleId);
            if (result == true)
            {
                persistenceContext.SaveChanges();
                return true;
            }

            return false;
        }

        public Vehicle Update(string id,
                              string name,
                              string type,
                              string registrationNumber,
                              int maximCarryWeight,
                              string vin)
        {
            var vehicleToUpdate = GetById(id);
            vehicleToUpdate.Update(name, type, registrationNumber, maximCarryWeight, vin);
            persistenceContext.SaveChanges();
            return vehicleToUpdate;
        }
    }
}
