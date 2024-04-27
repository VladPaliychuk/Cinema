using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.API.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Genre", "Description", "ImageFile", "Name", "Price", "Summary" },
                values: new object[,]
                {
                    { new Guid("97846f14-c173-4240-8c04-6c84c99f081a"), "Thriller", null, null, "Very Enigmatic Movie", 150m, null },
                    { new Guid("a166c338-5dac-4867-ae27-9431a35ad36c"), "Comedy", null, null, "Very Funny Movie", 120m, null },
                    { new Guid("e407e3cf-e9bb-4ad2-b623-ca2c2e02b0cd"), "Horror", null, null, "Very Scary Movie", 120m, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("97846f14-c173-4240-8c04-6c84c99f081a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a166c338-5dac-4867-ae27-9431a35ad36c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e407e3cf-e9bb-4ad2-b623-ca2c2e02b0cd"));
        }
    }
}
