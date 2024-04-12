using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_ColumnLength_ArmsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ITF_14",
                schema: "REF_1C",
                table: "PLUS",
                type: "varchar(144)",
                maxLength: 144,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "EAN_13",
                schema: "REF_1C",
                table: "PLUS",
                type: "varchar(133)",
                maxLength: 133,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "TYPE",
                table: "ARMS",
                type: "varchar(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)");

            migrationBuilder.AlterColumn<string>(
                name: "PC_NAME",
                table: "ARMS",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ITF_14",
                schema: "REF_1C",
                table: "PLUS",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(144)",
                oldMaxLength: 144);

            migrationBuilder.AlterColumn<string>(
                name: "EAN_13",
                schema: "REF_1C",
                table: "PLUS",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(133)",
                oldMaxLength: 133);

            migrationBuilder.AlterColumn<string>(
                name: "TYPE",
                table: "ARMS",
                type: "varchar(8)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)");

            migrationBuilder.AlterColumn<string>(
                name: "PC_NAME",
                table: "ARMS",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);
        }
    }
}