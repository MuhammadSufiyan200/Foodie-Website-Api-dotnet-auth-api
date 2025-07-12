using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication_System_API.Migrations
{
    /// <inheritdoc />
    public partial class forupdatedatabaseerror500 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceInfo",
                table: "Tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceInfo",
                table: "Tbl_ActiveSessions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Tbl_ActiveSessions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Tbl_ActiveSessions",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ActiveSessions_UserId",
                table: "Tbl_ActiveSessions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_ActiveSessions_Tbl_User_UserId",
                table: "Tbl_ActiveSessions",
                column: "UserId",
                principalTable: "Tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_ActiveSessions_Tbl_User_UserId",
                table: "Tbl_ActiveSessions");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_ActiveSessions_UserId",
                table: "Tbl_ActiveSessions");

            migrationBuilder.DropColumn(
                name: "DeviceInfo",
                table: "Tbl_User");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Tbl_User");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Tbl_User");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Tbl_ActiveSessions");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Tbl_ActiveSessions");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceInfo",
                table: "Tbl_ActiveSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
