using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_ArmsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PRINTERS_PRINTER_UID",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PLUS_FK_ARMS_ARM_UID",
                table: "ARMS_PLUS_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PLUS_FK_PLUS_PLU_UID",
                table: "ARMS_PLUS_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__ARM",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__PALLET",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__PLU",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS__BARCODE_TOP",
                schema: "PRINT",
                table: "LABELS",
                newName: "UQ_LABELS__BARCODE_TOP");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS_PC_NAME",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS__PC_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS_NUMBER",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS__NUMBER");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS_NAME",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS__NAME");

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS__PRINTER",
                schema: "REF",
                table: "ARMS",
                column: "PRINTER_UID",
                principalSchema: "REF",
                principalTable: "PRINTERS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS__WAREHOUSE",
                schema: "REF",
                table: "ARMS",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PLUS__ARM",
                table: "ARMS_PLUS_FK",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PLUS__PLU",
                table: "ARMS_PLUS_FK",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS__ARM",
                schema: "PRINT",
                table: "LABELS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS__PALLET",
                schema: "PRINT",
                table: "LABELS",
                column: "PALLET_UID",
                principalSchema: "PRINT",
                principalTable: "PALLETS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS__PLU",
                schema: "PRINT",
                table: "LABELS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ARMS__PRINTER",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS__WAREHOUSE",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PLUS__ARM",
                table: "ARMS_PLUS_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PLUS__PLU",
                table: "ARMS_PLUS_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_LABELS__ARM",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.DropForeignKey(
                name: "FK_LABELS__PALLET",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.DropForeignKey(
                name: "FK_LABELS__PLU",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.RenameIndex(
                name: "UQ_LABELS__BARCODE_TOP",
                schema: "PRINT",
                table: "LABELS",
                newName: "UQ_PLUS__BARCODE_TOP");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS__PC_NAME",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS_PC_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS__NUMBER",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS_NUMBER");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS__NAME",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS_NAME");

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PRINTERS_PRINTER_UID",
                schema: "REF",
                table: "ARMS",
                column: "PRINTER_UID",
                principalSchema: "REF",
                principalTable: "PRINTERS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "ARMS",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PLUS_FK_ARMS_ARM_UID",
                table: "ARMS_PLUS_FK",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PLUS_FK_PLUS_PLU_UID",
                table: "ARMS_PLUS_FK",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__ARM",
                schema: "PRINT",
                table: "LABELS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__PALLET",
                schema: "PRINT",
                table: "LABELS",
                column: "PALLET_UID",
                principalSchema: "PRINT",
                principalTable: "PALLETS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__PLU",
                schema: "PRINT",
                table: "LABELS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
