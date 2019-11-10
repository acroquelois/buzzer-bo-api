using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace buzzerApi.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionTexte",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Question = table.Column<string>(nullable: false),
                    Reponse = table.Column<string>(nullable: false),
                    Proposition1 = table.Column<string>(nullable: false),
                    Proposition2 = table.Column<string>(nullable: false),
                    Proposition3 = table.Column<string>(nullable: false),
                    Proposition4 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTexte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionTexte");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
