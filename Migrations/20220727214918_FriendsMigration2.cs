using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HokmGame_Server.Migrations
{
    public partial class FriendsMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Users_FriendId",
                table: "Friend");

            migrationBuilder.DropIndex(
                name: "IX_Friend_FriendId",
                table: "Friend");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Friend",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Friend_UserId1",
                table: "Friend",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Users_UserId1",
                table: "Friend",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Users_UserId1",
                table: "Friend");

            migrationBuilder.DropIndex(
                name: "IX_Friend_UserId1",
                table: "Friend");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Friend");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_FriendId",
                table: "Friend",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Users_FriendId",
                table: "Friend",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
