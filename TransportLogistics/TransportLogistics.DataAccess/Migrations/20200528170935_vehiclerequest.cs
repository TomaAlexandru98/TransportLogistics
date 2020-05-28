using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class vehiclerequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ContactDetailsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipients_Contact_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleChangeRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OldVehicleId = table.Column<Guid>(nullable: true),
                    NewVehicleId = table.Column<Guid>(nullable: true),
                    DriverId = table.Column<Guid>(nullable: true),
                    DispatcherId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleChangeRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleChangeRequests_Dispatchers_DispatcherId",
                        column: x => x.DispatcherId,
                        principalTable: "Dispatchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleChangeRequests_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleChangeRequests_Vehicles_NewVehicleId",
                        column: x => x.NewVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleChangeRequests_Vehicles_OldVehicleId",
                        column: x => x.OldVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_ContactDetailsId",
                table: "Recipients",
                column: "ContactDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleChangeRequests_DispatcherId",
                table: "VehicleChangeRequests",
                column: "DispatcherId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleChangeRequests_DriverId",
                table: "VehicleChangeRequests",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleChangeRequests_NewVehicleId",
                table: "VehicleChangeRequests",
                column: "NewVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleChangeRequests_OldVehicleId",
                table: "VehicleChangeRequests",
                column: "OldVehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "VehicleChangeRequests");
        }
    }
}
