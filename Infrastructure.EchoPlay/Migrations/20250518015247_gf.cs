using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EchoPlay.Migrations
{
    /// <inheritdoc />
    public partial class gf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_Users_UserId",
                table: "Codes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "TmpUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "TmpUsers",
                newName: "IX_TmpUsers_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Phone",
                table: "TmpUsers",
                newName: "IX_TmpUsers_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "TmpUsers",
                newName: "IX_TmpUsers_Email");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "TmpUsers",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TmpUsers",
                table: "TmpUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_TmpUsers_UserId",
                table: "Codes",
                column: "UserId",
                principalTable: "TmpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_TmpUsers_UserId",
                table: "Codes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TmpUsers",
                table: "TmpUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "TmpUsers");

            migrationBuilder.RenameTable(
                name: "TmpUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_TmpUsers_Username",
                table: "Users",
                newName: "IX_Users_Username");

            migrationBuilder.RenameIndex(
                name: "IX_TmpUsers_Phone",
                table: "Users",
                newName: "IX_Users_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_TmpUsers_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_Users_UserId",
                table: "Codes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
