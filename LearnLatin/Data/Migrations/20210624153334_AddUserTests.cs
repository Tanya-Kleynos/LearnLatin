using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLatin.Data.Migrations
{
    public partial class AddUserTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTests",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    TestId = table.Column<Guid>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true),
                    LastResult = table.Column<int>(nullable: true),
                    BestResult = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTests", x => new { x.UserId, x.TestId });
                    table.ForeignKey(
                        name: "FK_UserTests_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTests_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_TestId",
                table: "UserTests",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_UserId1",
                table: "UserTests",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTests");
        }
    }
}
