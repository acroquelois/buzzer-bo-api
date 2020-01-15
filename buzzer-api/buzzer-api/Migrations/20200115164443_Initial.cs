using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace buzzerApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionType",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionType", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Interogation = table.Column<string>(nullable: false),
                    Reponse = table.Column<string>(nullable: false),
                    QuestionTypeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_QuestionType_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proposition",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    QuestionId = table.Column<byte[]>(nullable: false),
                    proposition = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposition_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "QuestionType",
                columns: new[] { "Id", "Type" },
                values: new object[] { "TEXTE", "texte" });

            migrationBuilder.InsertData(
                table: "QuestionType",
                columns: new[] { "Id", "Type" },
                values: new object[] { "AUDIO", "audio" });

            migrationBuilder.InsertData(
                table: "QuestionType",
                columns: new[] { "Id", "Type" },
                values: new object[] { "IMAGE", "image" });

            migrationBuilder.CreateIndex(
                name: "IX_Proposition_QuestionId",
                table: "Proposition",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proposition");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "QuestionType");
        }
    }
}
