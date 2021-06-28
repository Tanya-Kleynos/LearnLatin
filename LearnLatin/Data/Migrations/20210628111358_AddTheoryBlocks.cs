using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLatin.Data.Migrations
{
    public partial class AddTheoryBlocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TheoryBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    ThemeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheoryBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheoryBlocks_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TheoryBlocks_ThemeId",
                table: "TheoryBlocks",
                column: "ThemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TheoryBlocks");
        }
    }
}
