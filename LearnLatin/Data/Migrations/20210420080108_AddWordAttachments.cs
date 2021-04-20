using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLatin.Data.Migrations
{
    public partial class AddWordAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LatinWordId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Path = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordAttachments_LatinWords_LatinWordId",
                        column: x => x.LatinWordId,
                        principalTable: "LatinWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordAttachments_LatinWordId",
                table: "WordAttachments",
                column: "LatinWordId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordAttachments");
        }
    }
}
