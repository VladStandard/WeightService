using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_PalletMenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PALLET_MEN_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                schema: "REF",
                table: "PALLET_MEN",
                newName: "UQ_PALLET_MEN__UID_1C");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN_FIO",
                schema: "REF",
                table: "PALLET_MEN",
                newName: "UQ_PALLET_MEN__FIO");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLET_MEN__WAREHOUSE",
                schema: "REF",
                table: "PALLET_MEN",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PALLET_MEN__WAREHOUSE",
                schema: "REF",
                table: "PALLET_MEN");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN__UID_1C",
                schema: "REF",
                table: "PALLET_MEN",
                newName: "UQ_PALLET_MEN_UID_1C");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN__FIO",
                schema: "REF",
                table: "PALLET_MEN",
                newName: "UQ_PALLET_MEN_FIO");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLET_MEN_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
