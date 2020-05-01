using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Data;

namespace TransportLogistics.DataAccess
{
    public class TransportLogisticsDbContext : DbContext
    {
        public TransportLogisticsDbContext(DbContextOptions<TransportLogisticsDbContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleData> Vehicles { get; set; }
        public DbSet<TrailerData> Trailers { get; set; }
        public DbSet<CustomerData> Customers { get; set; }


    }
}
