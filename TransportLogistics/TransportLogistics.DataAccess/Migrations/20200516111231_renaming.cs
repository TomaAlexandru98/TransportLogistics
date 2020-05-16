using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class renaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_RoutesHistories_RoutesHistoryId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_RoutesHistoryId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "RoutesHistoryId",
                table: "Drivers");

            migrationBuilder.AddColumn<Guid>(
                name: "RoutesHistoricId",
                table: "Drivers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_RoutesHistoricId",
                table: "Drivers",
                column: "RoutesHistoricId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_RoutesHistories_RoutesHistoricId",
                table: "Drivers",
                column: "RoutesHistoricId",
                principalTable: "RoutesHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_RoutesHistories_RoutesHistoricId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_RoutesHistoricId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "RoutesHistoricId",
                table: "Drivers");

            migrationBuilder.AddColumn<Guid>(
                name: "RoutesHistoryId",
                table: "Drivers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_RoutesHistoryId",
                table: "Drivers",
                column: "RoutesHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_RoutesHistories_RoutesHistoryId",
                table: "Drivers",
                column: "RoutesHistoryId",
                principalTable: "RoutesHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}