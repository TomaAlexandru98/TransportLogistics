﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class IntialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PhoneNo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    MaximCarryWeightKg = table.Column<int>(nullable: false),
                    VIN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ContactDetailsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Contact_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trailers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MaximWeightKg = table.Column<int>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    NumberAxles = table.Column<int>(nullable: false),
                    Height = table.Column<decimal>(nullable: false),
                    Width = table.Column<decimal>(nullable: false),
                    Length = table.Column<decimal>(nullable: false),
                    VehicleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trailers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationAddress_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ContactDetailsId",
                table: "Customers",
                column: "ContactDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationAddress_CustomerId",
                table: "LocationAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_VehicleId",
                table: "Trailers",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationAddress");

            migrationBuilder.DropTable(
                name: "Trailers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}