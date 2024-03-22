using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddStoraheMethodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ZPL",
                table: "ZPL_RESOURCES",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "SURNAME",
                table: "PALLET_MEN",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "PATRONYMIC",
                table: "PALLET_MEN",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                table: "PALLET_MEN",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.CreateTable(
                name: "STORAGE_METHODS",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STORAGE_METHODS", x => x.UID);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS_NAME",
                table: "STORAGE_METHODS",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS_ZPL",
                table: "STORAGE_METHODS",
                column: "DESCRIPTION");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STORAGE_METHODS");

            migrationBuilder.AlterColumn<string>(
                name: "ZPL",
                table: "ZPL_RESOURCES",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "SURNAME",
                table: "PALLET_MEN",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "PATRONYMIC",
                table: "PALLET_MEN",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                table: "PALLET_MEN",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);
        }
    }
}