using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_PrintersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRINTERS_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "PRINTERS");

            migrationBuilder.RenameColumn(
                name: "IP_V4",
                schema: "REF",
                table: "PRINTERS",
                newName: "IP");

            migrationBuilder.RenameIndex(
                name: "UQ_PRINTERS_NAME",
                schema: "REF",
                table: "PRINTERS",
                newName: "UQ_PRINTERS__NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_PRINTERS_IP_V4",
                schema: "REF",
                table: "PRINTERS",
                newName: "UQ_PALLET_MEN__IP");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLET_MEN__PRODUCTION_SITE",
                schema: "REF",
                table: "PRINTERS",
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
                name: "FK_PALLET_MEN__PRODUCTION_SITE",
                schema: "REF",
                table: "PRINTERS");

            migrationBuilder.RenameColumn(
                name: "IP",
                schema: "REF",
                table: "PRINTERS",
                newName: "IP_V4");

            migrationBuilder.RenameIndex(
                name: "UQ_PRINTERS__NAME",
                schema: "REF",
                table: "PRINTERS",
                newName: "UQ_PRINTERS_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN__IP",
                schema: "REF",
                table: "PRINTERS",
                newName: "UQ_PRINTERS_IP_V4");

            migrationBuilder.AddForeignKey(
                name: "FK_PRINTERS_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "PRINTERS",
                column: "PRODUCTION_SITE_UID",
                principalSchema: "REF",
                principalTable: "PRODUCTION_SITES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
