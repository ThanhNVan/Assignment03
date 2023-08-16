using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment03.EntityProviders.Migrations
{
    public partial class addEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPhone_User_UserId",
                table: "UserPhone");

            migrationBuilder.DropIndex(
                name: "IX_UserPhone_Phone_UserId",
                table: "UserPhone");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserPhone",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPhone_Phone_UserId",
                table: "UserPhone",
                columns: new[] { "Phone", "UserId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhone_User_UserId",
                table: "UserPhone",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPhone_User_UserId",
                table: "UserPhone");

            migrationBuilder.DropIndex(
                name: "IX_UserPhone_Phone_UserId",
                table: "UserPhone");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserPhone",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhone_Phone_UserId",
                table: "UserPhone",
                columns: new[] { "Phone", "UserId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhone_User_UserId",
                table: "UserPhone",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
