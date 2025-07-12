using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication_System_API.Migrations
{
    /// <inheritdoc />
    public partial class AddDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_SystemLogs_Tbl_User_UserId",
                table: "Tbl_SystemLogs");

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderUserId",
                table: "Tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Tbl_SystemLogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_SystemLogs_Tbl_User_UserId",
                table: "Tbl_SystemLogs",
                column: "UserId",
                principalTable: "Tbl_User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_SystemLogs_Tbl_User_UserId",
                table: "Tbl_SystemLogs");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Tbl_User");

            migrationBuilder.DropColumn(
                name: "ProviderUserId",
                table: "Tbl_User");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Tbl_SystemLogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_SystemLogs_Tbl_User_UserId",
                table: "Tbl_SystemLogs",
                column: "UserId",
                principalTable: "Tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
