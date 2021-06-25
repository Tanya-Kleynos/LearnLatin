using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLatin.Data.Migrations
{
    public partial class AddTasksUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnsweredRight",
                table: "TrueOutOfFalseTasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnsweredRight",
                table: "InputTasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnsweredRight",
                table: "TrueOutOfFalseTasks");

            migrationBuilder.DropColumn(
                name: "IsAnsweredRight",
                table: "InputTasks");
        }
    }
}
