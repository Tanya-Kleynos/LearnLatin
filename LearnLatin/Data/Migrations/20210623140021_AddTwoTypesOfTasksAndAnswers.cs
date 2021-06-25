using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnLatin.Data.Migrations
{
    public partial class AddTwoTypesOfTasksAndAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    EditorId = table.Column<string>(nullable: true),
                    NumOfTasks = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_AspNetUsers_EditorId",
                        column: x => x.EditorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InputTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    EditorId = table.Column<string>(nullable: true),
                    TestId = table.Column<Guid>(nullable: true),
                    NumInQueue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputTasks_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InputTasks_AspNetUsers_EditorId",
                        column: x => x.EditorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InputTasks_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrueOutOfFalseTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    EditorId = table.Column<string>(nullable: true),
                    TestId = table.Column<Guid>(nullable: true),
                    NumInQueue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrueOutOfFalseTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrueOutOfFalseTasks_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrueOutOfFalseTasks_AspNetUsers_EditorId",
                        column: x => x.EditorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrueOutOfFalseTasks_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InputAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    EditorId = table.Column<string>(nullable: true),
                    TaskId = table.Column<Guid>(nullable: true),
                    AnsValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputAnswers_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InputAnswers_AspNetUsers_EditorId",
                        column: x => x.EditorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InputAnswers_InputTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "InputTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrueOutOfFalseAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    EditorId = table.Column<string>(nullable: true),
                    TaskId = table.Column<Guid>(nullable: true),
                    AnsValue = table.Column<string>(nullable: true),
                    IsTrue = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrueOutOfFalseAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrueOutOfFalseAnswers_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrueOutOfFalseAnswers_AspNetUsers_EditorId",
                        column: x => x.EditorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrueOutOfFalseAnswers_TrueOutOfFalseTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TrueOutOfFalseTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InputAnswers_CreatorId",
                table: "InputAnswers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_InputAnswers_EditorId",
                table: "InputAnswers",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_InputAnswers_TaskId",
                table: "InputAnswers",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_InputTasks_CreatorId",
                table: "InputTasks",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_InputTasks_EditorId",
                table: "InputTasks",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_InputTasks_TestId",
                table: "InputTasks",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_CreatorId",
                table: "Tests",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_EditorId",
                table: "Tests",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueOutOfFalseAnswers_CreatorId",
                table: "TrueOutOfFalseAnswers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueOutOfFalseAnswers_EditorId",
                table: "TrueOutOfFalseAnswers",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueOutOfFalseAnswers_TaskId",
                table: "TrueOutOfFalseAnswers",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueOutOfFalseTasks_CreatorId",
                table: "TrueOutOfFalseTasks",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueOutOfFalseTasks_EditorId",
                table: "TrueOutOfFalseTasks",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueOutOfFalseTasks_TestId",
                table: "TrueOutOfFalseTasks",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputAnswers");

            migrationBuilder.DropTable(
                name: "TrueOutOfFalseAnswers");

            migrationBuilder.DropTable(
                name: "InputTasks");

            migrationBuilder.DropTable(
                name: "TrueOutOfFalseTasks");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
