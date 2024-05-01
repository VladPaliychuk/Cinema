using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.DAL.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgeRestriction",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeRestriction",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Products");
        }
    }
}
