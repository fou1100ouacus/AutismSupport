using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChildProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotherId = table.Column<int>(type: "int", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AgeInYears = table.Column<int>(type: "int", nullable: false),
                    AgeInMonths = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    SupportNeedsLevel = table.Column<int>(type: "int", nullable: false),
                    MainDailyChallengesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StrengthsAndInterests = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrefersVisualSchedules = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CommunicationMethodsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildProfiles_AspNetUsers_MotherId",
                        column: x => x.MotherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildProfiles_CreatedAt",
                table: "ChildProfiles",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ChildProfiles_MotherId",
                table: "ChildProfiles",
                column: "MotherId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildProfiles");
        }
    }
}
