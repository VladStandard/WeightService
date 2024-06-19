using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_WarehousesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WAREHOUSES_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "WAREHOUSES");

            migrationBuilder.RenameIndex(
                name: "UQ_WAREHOUSES_UID_1C",
                schema: "REF",
                table: "WAREHOUSES",
                newName: "UQ_WAREHOUSES__UID_1C");

            migrationBuilder.RenameIndex(
                name: "UQ_WAREHOUSES_NAME",
                schema: "REF",
                table: "WAREHOUSES",
                newName: "UQ_WAREHOUSES__NAME");

            migrationBuilder.AddForeignKey(
                name: "FK_WAREHOUSES__PRODUCTION_SITE",
                schema: "REF",
                table: "WAREHOUSES",
                column: "PRODUCTION_SITE_UID",
                principalSchema: "REF",
                principalTable: "PRODUCTION_SITES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WAREHOUSES__PRODUCTION_SITE",
                schema: "REF",
                table: "WAREHOUSES");

            migrationBuilder.RenameIndex(
                name: "UQ_WAREHOUSES__UID_1C",
                schema: "REF",
                table: "WAREHOUSES",
                newName: "UQ_WAREHOUSES_UID_1C");

            migrationBuilder.RenameIndex(
                name: "UQ_WAREHOUSES__NAME",
                schema: "REF",
                table: "WAREHOUSES",
                newName: "UQ_WAREHOUSES_NAME");

            migrationBuilder.AddForeignKey(
                name: "FK_WAREHOUSES_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "WAREHOUSES",
                column: "PRODUCTION_SITE_UID",
                principalSchema: "REF",
                principalTable: "PRODUCTION_SITES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
