using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AIMotion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotionAnalysisResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    Prediction = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SmmPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SmmSegmentsCount = table.Column<int>(type: "int", nullable: false),
                    VideoDuration = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotionAnalysisResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotionAnalysisResults_ChildProfiles_ChildId",
                        column: x => x.ChildId,
                        principalTable: "ChildProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MotionAnalysisResults_ChildId",
                table: "MotionAnalysisResults",
                column: "ChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotionAnalysisResults");
        }
    }
}
