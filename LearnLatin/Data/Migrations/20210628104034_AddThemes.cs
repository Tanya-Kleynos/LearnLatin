using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLatin.Data.Migrations
{
    public partial class AddThemes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ThemeId",
                table: "Tests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    EditorId = table.Column<string>(nullable: true),
                    NumOfTests = table.Column<int>(nullable: true),
                    PercentageProgress = table.Column<double>(nullable: true),
                    ParentThemeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Themes_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Themes_AspNetUsers_EditorId",
                        column: x => x.EditorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Themes_Themes_ParentThemeId",
                        column: x => x.ParentThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ThemeId",
                table: "Tests",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Themes_CreatorId",
                table: "Themes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Themes_EditorId",
                table: "Themes",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Themes_ParentThemeId",
                table: "Themes",
                column: "ParentThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Themes_ThemeId",
                table: "Tests",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Themes_ThemeId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_Tests_ThemeId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Tests");
        }
    }
}
