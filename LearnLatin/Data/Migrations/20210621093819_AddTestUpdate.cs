using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLatin.Data.Migrations
{
    public partial class AddTestUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestId",
                table: "TestTasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTasks_TestId",
                table: "TestTasks",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestTasks_Tests_TestId",
                table: "TestTasks",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestTasks_Tests_TestId",
                table: "TestTasks");

            migrationBuilder.DropIndex(
                name: "IX_TestTasks_TestId",
                table: "TestTasks");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "TestTasks");
        }
    }
}
