using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace buzzerApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionTexte",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    question = table.Column<string>(nullable: false),
                    reponse = table.Column<string>(nullable: false),
                    proposition1 = table.Column<string>(nullable: false),
                    proposition2 = table.Column<string>(nullable: false),
                    proposition3 = table.Column<string>(nullable: false),
                    proposition4 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTexte", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionTexte");
        }
    }
}
