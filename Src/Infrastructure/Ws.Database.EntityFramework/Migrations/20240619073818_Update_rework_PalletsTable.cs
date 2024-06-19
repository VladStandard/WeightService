using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_PalletsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS_ARMS_ARM_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS_PALLET_MEN_PALLET_MAN_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS_PLUS_PLU_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropIndex(
                name: "IX_PALLETS_PLU_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropColumn(
                name: "PLU_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.RenameColumn(
                name: "TRAY_WEIGHT",
                schema: "PRINT",
                table: "PALLETS",
                newName: "WEIGHT_TRAY");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLETS_COUNTER",
                schema: "PRINT",
                table: "PALLETS",
                newName: "UQ_PALLETS__COUNTER");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLETS_BARCODE",
                schema: "PRINT",
                table: "PALLETS",
                newName: "UQ_PALLETS__BARCODE");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS__ARM",
                schema: "PRINT",
                table: "PALLETS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS__PALLET_MAN",
                schema: "PRINT",
                table: "PALLETS",
                column: "PALLET_MAN_UID",
                principalSchema: "REF",
                principalTable: "PALLET_MEN",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS__ARM",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS__PALLET_MAN",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.RenameColumn(
                name: "WEIGHT_TRAY",
                schema: "PRINT",
                table: "PALLETS",
                newName: "TRAY_WEIGHT");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLETS__COUNTER",
                schema: "PRINT",
                table: "PALLETS",
                newName: "UQ_PALLETS_COUNTER");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLETS__BARCODE",
                schema: "PRINT",
                table: "PALLETS",
                newName: "UQ_PALLETS_BARCODE");

            migrationBuilder.AddColumn<Guid>(
                name: "PLU_UID",
                schema: "PRINT",
                table: "PALLETS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PALLETS_PLU_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "PLU_UID");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS_ARMS_ARM_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS_PALLET_MEN_PALLET_MAN_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "PALLET_MAN_UID",
                principalSchema: "REF",
                principalTable: "PALLET_MEN",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS_PLUS_PLU_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
