using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class DriverRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Recipients_RecipientId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Routes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_RecipientId",
                table: "Orders",
                column: "RecipientId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_RecipientId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Routes");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Recipients_RecipientId",
                table: "Orders",
                column: "RecipientId",
                principalTable: "Recipients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
