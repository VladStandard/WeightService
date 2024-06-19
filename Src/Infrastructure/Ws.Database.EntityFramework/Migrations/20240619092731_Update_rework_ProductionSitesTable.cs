using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_ProductionSitesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_PRODUCTION_SITES_NAME",
                schema: "REF",
                table: "PRODUCTION_SITES",
                newName: "UQ_PRODUCTION_SITES__NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_PRODUCTION_SITES_ADDRESS",
                schema: "REF",
                table: "PRODUCTION_SITES",
                newName: "UQ_PRODUCTION_SITES__ADDRESS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_PRODUCTION_SITES__NAME",
                schema: "REF",
                table: "PRODUCTION_SITES",
                newName: "UQ_PRODUCTION_SITES_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_PRODUCTION_SITES__ADDRESS",
                schema: "REF",
                table: "PRODUCTION_SITES",
                newName: "UQ_PRODUCTION_SITES_ADDRESS");
        }
    }
}
