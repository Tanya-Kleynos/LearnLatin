using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLatin.Data.Migrations
{
    public partial class AddDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputAnswers_InputTasks_TaskId",
                table: "InputAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_InputTasks_Tests_TestId",
                table: "InputTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TrueOutOfFalseAnswers_TrueOutOfFalseTasks_TaskId",
                table: "TrueOutOfFalseAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_TrueOutOfFalseTasks_Tests_TestId",
                table: "TrueOutOfFalseTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_InputAnswers_InputTasks_TaskId",
                table: "InputAnswers",
                column: "TaskId",
                principalTable: "InputTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InputTasks_Tests_TestId",
                table: "InputTasks",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrueOutOfFalseAnswers_TrueOutOfFalseTasks_TaskId",
                table: "TrueOutOfFalseAnswers",
                column: "TaskId",
                principalTable: "TrueOutOfFalseTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrueOutOfFalseTasks_Tests_TestId",
                table: "TrueOutOfFalseTasks",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputAnswers_InputTasks_TaskId",
                table: "InputAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_InputTasks_Tests_TestId",
                table: "InputTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TrueOutOfFalseAnswers_TrueOutOfFalseTasks_TaskId",
                table: "TrueOutOfFalseAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_TrueOutOfFalseTasks_Tests_TestId",
                table: "TrueOutOfFalseTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_InputAnswers_InputTasks_TaskId",
                table: "InputAnswers",
                column: "TaskId",
                principalTable: "InputTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InputTasks_Tests_TestId",
                table: "InputTasks",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrueOutOfFalseAnswers_TrueOutOfFalseTasks_TaskId",
                table: "TrueOutOfFalseAnswers",
                column: "TaskId",
                principalTable: "TrueOutOfFalseTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrueOutOfFalseTasks_Tests_TestId",
                table: "TrueOutOfFalseTasks",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
