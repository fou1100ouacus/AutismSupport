using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAbilitiesTrackerFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.CreateTable(
                name: "AbilityCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbilityCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbilityQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTextAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionTextEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbilityQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbilityQuestions_AbilityCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AbilityCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbilityTestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DetailedAnswersJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbilityTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbilityTestResults_AbilityCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AbilityCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbilityTestResults_ChildProfiles_ChildId",
                        column: x => x.ChildId,
                        principalTable: "ChildProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbilityQuestions_CategoryId",
                table: "AbilityQuestions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityTestResults_CategoryId",
                table: "AbilityTestResults",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityTestResults_ChildId",
                table: "AbilityTestResults",
                column: "ChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbilityQuestions");

            migrationBuilder.DropTable(
                name: "AbilityTestResults");

            migrationBuilder.DropTable(
                name: "AbilityCategories");

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    StudID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.StudID);
                });
        }
    }
}
