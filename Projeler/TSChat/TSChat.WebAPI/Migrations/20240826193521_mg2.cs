using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSChat.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_to_user_read",
                table: "chats",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_user_read",
                table: "chats",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_to_user_read",
                table: "chats");

            migrationBuilder.DropColumn(
                name: "is_user_read",
                table: "chats");
        }
    }
}
