using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
<<<<<<< HEAD:TransportLogistics/TransportLogistics.DataAccess/Migrations/20200520132831_initial.cs
    public partial class initial : Migration
=======
    public partial class Refactoring : Migration
>>>>>>> AlexToma:TransportLogistics/TransportLogistics.DataAccess/Migrations/20200519082841_Refactoring.cs
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
                name: "Dispatchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoutesHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutesHistories", x => x.Id);
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
                    VIN = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
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
                    Name = table.Column<string>(nullable: true),
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
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoutesHistoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_RoutesHistories_RoutesHistoryId",
                        column: x => x.RoutesHistoryId,
                        principalTable: "RoutesHistories",
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
                    Status = table.Column<int>(nullable: false),
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
                name: "LocationAddresses",
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
                    table.PrimaryKey("PK_LocationAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RoutesHistoricId = table.Column<Guid>(nullable: true),
                    CurrentRouteId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Routes_CurrentRouteId",
                        column: x => x.CurrentRouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Drivers_RoutesHistories_RoutesHistoricId",
                        column: x => x.RoutesHistoricId,
                        principalTable: "RoutesHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PickUpAddressId = table.Column<Guid>(nullable: true),
                    DeliveryAddressId = table.Column<Guid>(nullable: true),
                    RecipientId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_LocationAddresses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "LocationAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_LocationAddresses_PickUpAddressId",
                        column: x => x.PickUpAddressId,
                        principalTable: "LocationAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RouteEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    OrderType = table.Column<int>(nullable: false),
                    RouteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteEntries_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteEntries_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ContactDetailsId",
                table: "Customers",
                column: "ContactDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CurrentRouteId",
                table: "Drivers",
                column: "CurrentRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_RoutesHistoricId",
                table: "Drivers",
                column: "RoutesHistoricId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationAddresses_CustomerId",
                table: "LocationAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickUpAddressId",
                table: "Orders",
                column: "PickUpAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RecipientId",
                table: "Orders",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteEntries_OrderId",
                table: "RouteEntries",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteEntries_RouteId",
                table: "RouteEntries",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_RoutesHistoryId",
                table: "Routes",
                column: "RoutesHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_VehicleId",
                table: "Trailers",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dispatchers");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "RouteEntries");

            migrationBuilder.DropTable(
                name: "Trailers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "LocationAddresses");

            migrationBuilder.DropTable(
                name: "RoutesHistories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}
