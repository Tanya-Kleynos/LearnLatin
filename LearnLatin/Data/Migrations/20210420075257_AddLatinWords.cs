using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLatin.Data.Migrations
{
    public partial class AddLatinWords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LatinWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VocabularyUserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Translation = table.Column<string>(nullable: true),
                    Percent = table.Column<int>(nullable: false),
                    Training = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LatinWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LatinWords_VocabularyUsers_VocabularyUserId",
                        column: x => x.VocabularyUserId,
                        principalTable: "VocabularyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LatinWords_VocabularyUserId",
                table: "LatinWords",
                column: "VocabularyUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LatinWords");
        }
    }
}
