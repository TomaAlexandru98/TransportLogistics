using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class VehicleToRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VehicleId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_VehicleId",
                table: "Requests",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Vehicles_VehicleId",
                table: "Requests",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Vehicles_VehicleId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_VehicleId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Requests");
        }
    }
}
