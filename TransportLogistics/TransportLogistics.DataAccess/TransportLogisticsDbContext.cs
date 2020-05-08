using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess
{
    public class TransportLogisticsDbContext : DbContext
    {
      
        public TransportLogisticsDbContext(DbContextOptions<TransportLogisticsDbContext> options)
            : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Dispatcher> Dispatchers { get; set; }
        public DbSet<LocationAddress> LocationAddresses { get; set; }
    }
}
