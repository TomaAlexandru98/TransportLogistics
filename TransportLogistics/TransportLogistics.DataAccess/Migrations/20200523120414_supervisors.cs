using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportLogistics.DataAccess.Migrations
{
    public partial class supervisors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Supervisor_SupervisorId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supervisor",
                table: "Supervisor");

            migrationBuilder.RenameTable(
                name: "Supervisor",
                newName: "Supervisors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supervisors",
                table: "Supervisors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Supervisors_SupervisorId",
                table: "Requests",
                column: "SupervisorId",
                principalTable: "Supervisors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Supervisors_SupervisorId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supervisors",
                table: "Supervisors");

            migrationBuilder.RenameTable(
                name: "Supervisors",
                newName: "Supervisor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supervisor",
                table: "Supervisor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Supervisor_SupervisorId",
                table: "Requests",
                column: "SupervisorId",
                principalTable: "Supervisor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
