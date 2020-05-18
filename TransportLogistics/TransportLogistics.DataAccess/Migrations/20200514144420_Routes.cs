using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class Routes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationAddresses_Route_RouteId",
                table: "LocationAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Drivers_DriverId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Route_Drivers_DriverId",
                table: "Route");

            migrationBuilder.DropIndex(
                name: "IX_Route_DriverId",
                table: "Route");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DriverId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_LocationAddresses_RouteId",
                table: "LocationAddresses");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "LocationAddresses");

            migrationBuilder.AddColumn<Guid>(
                name: "RoutesHistoryId",
                table: "Route",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RouteId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentRouteId",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoutesHistoryId",
                table: "Drivers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoutesHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutesHistory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Route_RoutesHistoryId",
                table: "Route",
                column: "RoutesHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RouteId",
                table: "Orders",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CurrentRouteId",
                table: "Drivers",
                column: "CurrentRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_RoutesHistoryId",
                table: "Drivers",
                column: "RoutesHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Route_CurrentRouteId",
                table: "Drivers",
                column: "CurrentRouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_RoutesHistory_RoutesHistoryId",
                table: "Drivers",
                column: "RoutesHistoryId",
                principalTable: "RoutesHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Route_RouteId",
                table: "Orders",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Route_RoutesHistory_RoutesHistoryId",
                table: "Route",
                column: "RoutesHistoryId",
                principalTable: "RoutesHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Route_CurrentRouteId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_RoutesHistory_RoutesHistoryId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Route_RouteId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Route_RoutesHistory_RoutesHistoryId",
                table: "Route");

            migrationBuilder.DropTable(
                name: "RoutesHistory");

            migrationBuilder.DropIndex(
                name: "IX_Route_RoutesHistoryId",
                table: "Route");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RouteId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_CurrentRouteId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_RoutesHistoryId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "RoutesHistoryId",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CurrentRouteId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "RoutesHistoryId",
                table: "Drivers");

            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                table: "Route",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RouteId",
                table: "LocationAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Route_DriverId",
                table: "Route",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DriverId",
                table: "Orders",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationAddresses_RouteId",
                table: "LocationAddresses",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationAddresses_Route_RouteId",
                table: "LocationAddresses",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Drivers_DriverId",
                table: "Orders",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Route_Drivers_DriverId",
                table: "Route",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}