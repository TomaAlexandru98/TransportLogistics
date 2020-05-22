using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class Sender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SenderId",
                table: "Orders",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_SenderId",
                table: "Orders",
                column: "SenderId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_SenderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SenderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Orders");
        }
    }
}
