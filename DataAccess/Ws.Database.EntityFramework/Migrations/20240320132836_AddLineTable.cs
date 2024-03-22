using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddLineTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LINES",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    COUNTER = table.Column<int>(type: "int", nullable: false),
                    NUMBER = table.Column<int>(type: "int", nullable: false),
                    WAREHOUSE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PRINTER_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PC_NAME = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    TYPE = table.Column<string>(type: "varchar(8)", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LINES", x => x.UID);
                    table.ForeignKey(
                        name: "FK_LINES_PRINTERS_PRINTER_UID",
                        column: x => x.PRINTER_UID,
                        principalTable: "PRINTERS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LINES_WAREHOUSES_WAREHOUSE_UID",
                        column: x => x.WAREHOUSE_UID,
                        principalTable: "WAREHOUSES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LINES_PRINTER_UID",
                table: "LINES",
                column: "PRINTER_UID");

            migrationBuilder.CreateIndex(
                name: "IX_LINES_WAREHOUSE_UID",
                table: "LINES",
                column: "WAREHOUSE_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_LINES_NAME",
                table: "LINES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_LINES_NUMBER",
                table: "LINES",
                column: "NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_LINES_PC_NAME",
                table: "LINES",
                column: "PC_NAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LINES");
        }
    }
}