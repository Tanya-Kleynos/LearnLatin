using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLatin.Data.Migrations
{
    public partial class AddTestUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumOfRightAnswers",
                table: "Tests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfRightAnswers",
                table: "Tests");
        }
    }
}
