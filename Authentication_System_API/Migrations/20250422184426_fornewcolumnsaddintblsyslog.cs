using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication_System_API.Migrations
{
    /// <inheritdoc />
    public partial class fornewcolumnsaddintblsyslog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "Tbl_SystemLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Tbl_SystemLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogLevel",
                table: "Tbl_SystemLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_SystemLogs_UserId",
                table: "Tbl_SystemLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_SystemLogs_Tbl_User_UserId",
                table: "Tbl_SystemLogs",
                column: "UserId",
                principalTable: "Tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_SystemLogs_Tbl_User_UserId",
                table: "Tbl_SystemLogs");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_SystemLogs_UserId",
                table: "Tbl_SystemLogs");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Tbl_SystemLogs");

            migrationBuilder.DropColumn(
                name: "LogLevel",
                table: "Tbl_SystemLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "Tbl_SystemLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
