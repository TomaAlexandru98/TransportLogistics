using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class VehicleDriver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleDrivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VehicleId = table.Column<Guid>(nullable: true),
                    DriverId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDrivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleDrivers_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleDrivers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDrivers_DriverId",
                table: "VehicleDrivers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDrivers_VehicleId",
                table: "VehicleDrivers",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleDrivers");
        }
    }
}
