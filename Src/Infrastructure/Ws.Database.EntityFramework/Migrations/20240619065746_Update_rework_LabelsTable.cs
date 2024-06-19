using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_LabelsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LABELS_ARMS_ARM_UID",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.DropForeignKey(
                name: "FK_LABELS_PLUS_PLU_UID",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS_BARCODE_TOP",
                schema: "PRINT",
                table: "LABELS",
                newName: "UQ_PLUS__BARCODE_TOP");

            migrationBuilder.CreateIndex(
                name: "IX_LABELS_PALLET_UID",
                schema: "PRINT",
                table: "LABELS",
                column: "PALLET_UID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropIndex(
                name: "IX_LABELS_PALLET_UID",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS__BARCODE_TOP",
                schema: "PRINT",
                table: "LABELS",
                newName: "UQ_PLUS_BARCODE_TOP");

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS_ARMS_ARM_UID",
                schema: "PRINT",
                table: "LABELS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS_PLUS_PLU_UID",
                schema: "PRINT",
                table: "LABELS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID");
        }
    }
}
