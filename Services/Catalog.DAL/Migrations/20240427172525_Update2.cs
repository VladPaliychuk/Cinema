using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.DAL.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartScreening",
                table: "Screenings",
                newName: "StartTime");

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Screenings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Screenings");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Screenings",
                newName: "StartScreening");
        }
    }
}
