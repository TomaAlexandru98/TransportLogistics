using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Repositories
{
    class EFVehicleChangeRequestRepository:EFBaseRepository<VehicleChangeRequest>,IVehicleChangeRepository
    {
        public EFVehicleChangeRequestRepository(TransportLogisticsDbContext context) : base(context)
        {

        }
    }
}
