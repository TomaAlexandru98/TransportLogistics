using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class routeEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Route_RouteId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RouteId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "RouteEntryId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RouteEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderType = table.Column<int>(nullable: false),
                    RouteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteEntry_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RouteEntryId",
                table: "Orders",
                column: "RouteEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteEntry_RouteId",
                table: "RouteEntry",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_RouteEntry_RouteEntryId",
                table: "Orders",
                column: "RouteEntryId",
                principalTable: "RouteEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_RouteEntry_RouteEntryId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "RouteEntry");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RouteEntryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RouteEntryId",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "RouteId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RouteId",
                table: "Orders",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Route_RouteId",
                table: "Orders",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
