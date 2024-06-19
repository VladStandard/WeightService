using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_ZplResourcesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_RESOURCES_NAME",
                schema: "ZPL",
                table: "RESOURCES",
                newName: "UQ_RESOURCES__NAME");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_RESOURCES__NAME",
                schema: "ZPL",
                table: "RESOURCES",
                newName: "UQ_RESOURCES_NAME");
        }
    }
}
