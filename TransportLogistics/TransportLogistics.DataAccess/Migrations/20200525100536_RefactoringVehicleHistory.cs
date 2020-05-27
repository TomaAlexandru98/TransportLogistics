using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class RefactoringVehicleHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleDrivers_VehicleHistories_VehicleHistoryId",
                table: "VehicleDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleHistories_HistoryId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleHistories");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_HistoryId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_VehicleDrivers_VehicleHistoryId",
                table: "VehicleDrivers");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleHistoryId",
                table: "VehicleDrivers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HistoryId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleHistoryId",
                table: "VehicleDrivers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VehicleHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleHistories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_HistoryId",
                table: "Vehicles",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDrivers_VehicleHistoryId",
                table: "VehicleDrivers",
                column: "VehicleHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleDrivers_VehicleHistories_VehicleHistoryId",
                table: "VehicleDrivers",
                column: "VehicleHistoryId",
                principalTable: "VehicleHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleHistories_HistoryId",
                table: "Vehicles",
                column: "HistoryId",
                principalTable: "VehicleHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
