using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class routeEntryOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_RouteEntry_RouteEntryId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RouteEntryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RouteEntryId",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "RouteEntry",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteEntry_OrderId",
                table: "RouteEntry",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteEntry_Orders_OrderId",
                table: "RouteEntry",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteEntry_Orders_OrderId",
                table: "RouteEntry");

            migrationBuilder.DropIndex(
                name: "IX_RouteEntry_OrderId",
                table: "RouteEntry");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "RouteEntry");

            migrationBuilder.AddColumn<Guid>(
                name: "RouteEntryId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RouteEntryId",
                table: "Orders",
                column: "RouteEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_RouteEntry_RouteEntryId",
                table: "Orders",
                column: "RouteEntryId",
                principalTable: "RouteEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
