using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class OrdersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PickUpAddressId = table.Column<Guid>(nullable: true),
                    DeliveryAddressId = table.Column<Guid>(nullable: true),
                    RecipientId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_LocationAddresses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "LocationAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_LocationAddresses_PickUpAddressId",
                        column: x => x.PickUpAddressId,
                        principalTable: "LocationAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickUpAddressId",
                table: "Orders",
                column: "PickUpAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RecipientId",
                table: "Orders",
                column: "RecipientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
