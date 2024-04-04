using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserCard.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Bonuses = table.Column<int>(type: "integer", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    AddressLine = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    ZipCode = table.Column<string>(type: "text", nullable: true),
                    CardName = table.Column<string>(type: "text", nullable: true),
                    CardNumber = table.Column<string>(type: "text", nullable: true),
                    Expiration = table.Column<string>(type: "text", nullable: true),
                    CVV = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCards", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCards");
        }
    }
}
