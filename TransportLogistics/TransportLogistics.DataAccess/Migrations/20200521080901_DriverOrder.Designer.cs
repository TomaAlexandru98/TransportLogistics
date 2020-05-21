﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransportLogistics.DataAccess;

namespace TransportLogistics.DataAccess.Migrations
{
    [DbContext(typeof(TransportLogisticsDbContext))]
    [Migration("20200521080901_DriverOrder")]
    partial class DriverOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TransportLogistics.Model.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("TransportLogistics.Model.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContactDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactDetailsId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TransportLogistics.Model.Dispatcher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Dispatchers");
                });

            modelBuilder.Entity("TransportLogistics.Model.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CurrentRouteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoutesHistoricId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentRouteId");

                    b.HasIndex("RoutesHistoricId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("TransportLogistics.Model.LocationAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StreetNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("LocationAddresses");
                });

            modelBuilder.Entity("TransportLogistics.Model.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeliveryAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PickUpAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("RecipientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryAddressId");

                    b.HasIndex("PickUpAddressId");

                    b.HasIndex("RecipientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TransportLogistics.Model.Route", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoutesHistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoutesHistoryId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("TransportLogistics.Model.RouteEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrderType")
                        .HasColumnType("int");

                    b.Property<Guid?>("RouteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("RouteId");

                    b.ToTable("RouteEntries");
                });

            modelBuilder.Entity("TransportLogistics.Model.RoutesHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RoutesHistories");
                });

            modelBuilder.Entity("TransportLogistics.Model.Trailer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MaximWeightKg")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberAxles")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Trailers");
                });

            modelBuilder.Entity("TransportLogistics.Model.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaximCarryWeightKg")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("TransportLogistics.Model.VehicleDriver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleDrivers");
                });

            modelBuilder.Entity("TransportLogistics.Model.Customer", b =>
                {
                    b.HasOne("TransportLogistics.Model.Contact", "ContactDetails")
                        .WithMany()
                        .HasForeignKey("ContactDetailsId");
                });

            modelBuilder.Entity("TransportLogistics.Model.Driver", b =>
                {
                    b.HasOne("TransportLogistics.Model.Route", "CurrentRoute")
                        .WithMany()
                        .HasForeignKey("CurrentRouteId");

                    b.HasOne("TransportLogistics.Model.RoutesHistory", "RoutesHistoric")
                        .WithMany()
                        .HasForeignKey("RoutesHistoricId");
                });

            modelBuilder.Entity("TransportLogistics.Model.LocationAddress", b =>
                {
                    b.HasOne("TransportLogistics.Model.Customer", null)
                        .WithMany("LocationAddresses")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("TransportLogistics.Model.Order", b =>
                {
                    b.HasOne("TransportLogistics.Model.LocationAddress", "DeliveryAddress")
                        .WithMany()
                        .HasForeignKey("DeliveryAddressId");

                    b.HasOne("TransportLogistics.Model.LocationAddress", "PickUpAddress")
                        .WithMany()
                        .HasForeignKey("PickUpAddressId");

                    b.HasOne("TransportLogistics.Model.Customer", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId");
                });

            modelBuilder.Entity("TransportLogistics.Model.Route", b =>
                {
                    b.HasOne("TransportLogistics.Model.RoutesHistory", null)
                        .WithMany("Routes")
                        .HasForeignKey("RoutesHistoryId");

                    b.HasOne("TransportLogistics.Model.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("TransportLogistics.Model.RouteEntry", b =>
                {
                    b.HasOne("TransportLogistics.Model.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("TransportLogistics.Model.Route", null)
                        .WithMany("RouteEntries")
                        .HasForeignKey("RouteId");
                });

            modelBuilder.Entity("TransportLogistics.Model.Trailer", b =>
                {
                    b.HasOne("TransportLogistics.Model.Vehicle", null)
                        .WithMany("CurrentTrailers")
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("TransportLogistics.Model.VehicleDriver", b =>
                {
                    b.HasOne("TransportLogistics.Model.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId");

                    b.HasOne("TransportLogistics.Model.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");
                });
#pragma warning restore 612, 618
        }
    }
}
