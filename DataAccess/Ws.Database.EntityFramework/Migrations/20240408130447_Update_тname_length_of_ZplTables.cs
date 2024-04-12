using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_тname_length_of_ZplTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "ZPL",
                schema: "ZPL",
                table: "RESOURCES",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "ZPL",
                schema: "ZPL",
                table: "RESOURCES",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);
        }
    }
}