using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class editInfoRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EditPersonalInfoRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Applicant = table.Column<Guid>(nullable: false),
                    Administrator = table.Column<Guid>(nullable: false),
                    NewName = table.Column<string>(nullable: true),
                    NewEmail = table.Column<string>(nullable: true),
                    NewPhoneNumber = table.Column<string>(nullable: true),
                    OldName = table.Column<string>(nullable: true),
                    OldEmail = table.Column<string>(nullable: true),
                    OldPhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditPersonalInfoRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditPersonalInfoRequests");
        }
    }
}
