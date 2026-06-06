using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixReactionIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommunityReactions_UserId_PostId_CommentId_TargetType",
                table: "CommunityReactions");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityReactions_UserId_PostId_TargetType",
                table: "CommunityReactions",
                columns: new[] { "UserId", "PostId", "TargetType" },
                unique: true,
                filter: "[PostId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommunityReactions_UserId_PostId_TargetType",
                table: "CommunityReactions");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityReactions_UserId_PostId_CommentId_TargetType",
                table: "CommunityReactions",
                columns: new[] { "UserId", "PostId", "CommentId", "TargetType" },
                unique: true);
        }
    }
}
