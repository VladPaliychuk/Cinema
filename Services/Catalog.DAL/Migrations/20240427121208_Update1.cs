using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.API.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ReleaseDate",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartScreening = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductActors",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductActors", x => new { x.ProductId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_ProductActors_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductActors_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductGenres",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGenres", x => new { x.ProductId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_ProductGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductGenres_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Row = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    IsReserved = table.Column<bool>(type: "boolean", nullable: false),
                    ScreeningId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalTable: "Screenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductActors_ActorId",
                table: "ProductActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGenres_GenreId",
                table: "ProductGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_ProductId",
                table: "Screenings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ScreeningId",
                table: "Seats",
                column: "ScreeningId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductActors");

            migrationBuilder.DropTable(
                name: "ProductGenres");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Genre", "ImageFile", "Name", "Price", "Summary" },
                values: new object[,]
                {
                    { new Guid("97846f14-c173-4240-8c04-6c84c99f081a"), null, null, null, "Very Enigmatic Movie", 150m, null },
                    { new Guid("a166c338-5dac-4867-ae27-9431a35ad36c"), null, null, null, "Very Funny Movie", 120m, null },
                    { new Guid("e407e3cf-e9bb-4ad2-b623-ca2c2e02b0cd"), null, null, null, "Very Scary Movie", 120m, null }
                });
        }
    }
}
