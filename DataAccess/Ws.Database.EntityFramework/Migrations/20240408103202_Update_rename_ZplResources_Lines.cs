using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rename_ZplResources_Lines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LINES_PRINTERS_PRINTER_UID",
                table: "LINES");

            migrationBuilder.DropForeignKey(
                name: "FK_LINES_WAREHOUSES_WAREHOUSE_UID",
                table: "LINES");

            migrationBuilder.DropForeignKey(
                name: "FK_LINES_PLUS_FK_LINES_LINE_UID",
                table: "LINES_PLUS_FK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZPL_RESOURCES",
                schema: "ZPL",
                table: "ZPL_RESOURCES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LINES",
                table: "LINES");

            migrationBuilder.RenameTable(
                name: "ZPL_RESOURCES",
                schema: "ZPL",
                newName: "RESOURCES",
                newSchema: "ZPL");

            migrationBuilder.RenameTable(
                name: "LINES",
                newName: "ARMS");

            migrationBuilder.RenameIndex(
                name: "UQ_LINES_NUMBER",
                schema: "REF_1C",
                table: "PLUS",
                newName: "UQ_PLUS_NUMBER");

            migrationBuilder.RenameIndex(
                name: "UQ_ZPL_RESOURCES_NAME",
                schema: "ZPL",
                table: "RESOURCES",
                newName: "UQ_RESOURCES_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_LINES_PC_NAME",
                table: "ARMS",
                newName: "UQ_ARMS_PC_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_LINES_NUMBER",
                table: "ARMS",
                newName: "UQ_ARMS_NUMBER");

            migrationBuilder.RenameIndex(
                name: "UQ_LINES_NAME",
                table: "ARMS",
                newName: "UQ_ARMS_NAME");

            migrationBuilder.RenameIndex(
                name: "IX_LINES_WAREHOUSE_UID",
                table: "ARMS",
                newName: "IX_ARMS_WAREHOUSE_UID");

            migrationBuilder.RenameIndex(
                name: "IX_LINES_PRINTER_UID",
                table: "ARMS",
                newName: "IX_ARMS_PRINTER_UID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RESOURCES",
                schema: "ZPL",
                table: "RESOURCES",
                column: "UID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ARMS",
                table: "ARMS",
                column: "UID");

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PRINTERS_PRINTER_UID",
                table: "ARMS",
                column: "PRINTER_UID",
                principalTable: "PRINTERS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_WAREHOUSES_WAREHOUSE_UID",
                table: "ARMS",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LINES_PLUS_FK_ARMS_LINE_UID",
                table: "LINES_PLUS_FK",
                column: "LINE_UID",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PRINTERS_PRINTER_UID",
                table: "ARMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_WAREHOUSES_WAREHOUSE_UID",
                table: "ARMS");

            migrationBuilder.DropForeignKey(
                name: "FK_LINES_PLUS_FK_ARMS_LINE_UID",
                table: "LINES_PLUS_FK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RESOURCES",
                schema: "ZPL",
                table: "RESOURCES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ARMS",
                table: "ARMS");

            migrationBuilder.RenameTable(
                name: "RESOURCES",
                schema: "ZPL",
                newName: "ZPL_RESOURCES",
                newSchema: "ZPL");

            migrationBuilder.RenameTable(
                name: "ARMS",
                newName: "LINES");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS_NUMBER",
                schema: "REF_1C",
                table: "PLUS",
                newName: "UQ_LINES_NUMBER");

            migrationBuilder.RenameIndex(
                name: "UQ_RESOURCES_NAME",
                schema: "ZPL",
                table: "ZPL_RESOURCES",
                newName: "UQ_ZPL_RESOURCES_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS_PC_NAME",
                table: "LINES",
                newName: "UQ_LINES_PC_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS_NUMBER",
                table: "LINES",
                newName: "UQ_LINES_NUMBER");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS_NAME",
                table: "LINES",
                newName: "UQ_LINES_NAME");

            migrationBuilder.RenameIndex(
                name: "IX_ARMS_WAREHOUSE_UID",
                table: "LINES",
                newName: "IX_LINES_WAREHOUSE_UID");

            migrationBuilder.RenameIndex(
                name: "IX_ARMS_PRINTER_UID",
                table: "LINES",
                newName: "IX_LINES_PRINTER_UID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZPL_RESOURCES",
                schema: "ZPL",
                table: "ZPL_RESOURCES",
                column: "UID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LINES",
                table: "LINES",
                column: "UID");

            migrationBuilder.AddForeignKey(
                name: "FK_LINES_PRINTERS_PRINTER_UID",
                table: "LINES",
                column: "PRINTER_UID",
                principalTable: "PRINTERS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LINES_WAREHOUSES_WAREHOUSE_UID",
                table: "LINES",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LINES_PLUS_FK_LINES_LINE_UID",
                table: "LINES_PLUS_FK",
                column: "LINE_UID",
                principalTable: "LINES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}