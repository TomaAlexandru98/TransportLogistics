using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class routeEntryOr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Route_CurrentRouteId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_RoutesHistory_RoutesHistoryId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Route_RoutesHistory_RoutesHistoryId",
                table: "Route");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteEntry_Orders_OrderId",
                table: "RouteEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteEntry_Route_RouteId",
                table: "RouteEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutesHistory",
                table: "RoutesHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RouteEntry",
                table: "RouteEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Route",
                table: "Route");

            migrationBuilder.RenameTable(
                name: "RoutesHistory",
                newName: "RoutesHistories");

            migrationBuilder.RenameTable(
                name: "RouteEntry",
                newName: "RouteEntries");

            migrationBuilder.RenameTable(
                name: "Route",
                newName: "Routes");

            migrationBuilder.RenameIndex(
                name: "IX_RouteEntry_RouteId",
                table: "RouteEntries",
                newName: "IX_RouteEntries_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_RouteEntry_OrderId",
                table: "RouteEntries",
                newName: "IX_RouteEntries_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Route_RoutesHistoryId",
                table: "Routes",
                newName: "IX_Routes_RoutesHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutesHistories",
                table: "RoutesHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RouteEntries",
                table: "RouteEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Routes_CurrentRouteId",
                table: "Drivers",
                column: "CurrentRouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_RoutesHistories_RoutesHistoryId",
                table: "Drivers",
                column: "RoutesHistoryId",
                principalTable: "RoutesHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteEntries_Orders_OrderId",
                table: "RouteEntries",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteEntries_Routes_RouteId",
                table: "RouteEntries",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_RoutesHistories_RoutesHistoryId",
                table: "Routes",
                column: "RoutesHistoryId",
                principalTable: "RoutesHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Routes_CurrentRouteId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_RoutesHistories_RoutesHistoryId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteEntries_Orders_OrderId",
                table: "RouteEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteEntries_Routes_RouteId",
                table: "RouteEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_RoutesHistories_RoutesHistoryId",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutesHistories",
                table: "RoutesHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RouteEntries",
                table: "RouteEntries");

            migrationBuilder.RenameTable(
                name: "RoutesHistories",
                newName: "RoutesHistory");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "Route");

            migrationBuilder.RenameTable(
                name: "RouteEntries",
                newName: "RouteEntry");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_RoutesHistoryId",
                table: "Route",
                newName: "IX_Route_RoutesHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RouteEntries_RouteId",
                table: "RouteEntry",
                newName: "IX_RouteEntry_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_RouteEntries_OrderId",
                table: "RouteEntry",
                newName: "IX_RouteEntry_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutesHistory",
                table: "RoutesHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Route",
                table: "Route",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RouteEntry",
                table: "RouteEntry",
                column: "Id");

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
                name: "FK_Route_RoutesHistory_RoutesHistoryId",
                table: "Route",
                column: "RoutesHistoryId",
                principalTable: "RoutesHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteEntry_Orders_OrderId",
                table: "RouteEntry",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteEntry_Route_RouteId",
                table: "RouteEntry",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
