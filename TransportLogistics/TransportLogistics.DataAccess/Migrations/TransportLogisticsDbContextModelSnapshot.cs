﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransportLogistics.DataAccess;

namespace TransportLogistics.DataAccess.Migrations
{
    [DbContext(typeof(TransportLogisticsDbContext))]
    partial class TransportLogisticsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

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

                    b.Property<Guid?>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PickUpAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RecipientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryAddressId");

                    b.HasIndex("DriverId");

                    b.HasIndex("PickUpAddressId");

                    b.HasIndex("RecipientId");

                    b.ToTable("Orders");
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

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("TransportLogistics.Model.Customer", b =>
                {
                    b.HasOne("TransportLogistics.Model.Contact", "ContactDetails")
                        .WithMany()
                        .HasForeignKey("ContactDetailsId");
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

                    b.HasOne("TransportLogistics.Model.Driver", null)
                        .WithMany("Orders")
                        .HasForeignKey("DriverId");

                    b.HasOne("TransportLogistics.Model.LocationAddress", "PickUpAddress")
                        .WithMany()
                        .HasForeignKey("PickUpAddressId");

                    b.HasOne("TransportLogistics.Model.Customer", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId");
                });

            modelBuilder.Entity("TransportLogistics.Model.Trailer", b =>
                {
                    b.HasOne("TransportLogistics.Model.Vehicle", null)
                        .WithMany("CurrentTrailers")
                        .HasForeignKey("VehicleId");
                });
#pragma warning restore 612, 618
        }
    }
}
