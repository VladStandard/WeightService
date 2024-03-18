using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RenameZplResourcesTableToCaps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ZplResources",
                table: "ZplResources");

            migrationBuilder.RenameTable(
                name: "ZplResources",
                newName: "ZPL_RESOURCES");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZPL_RESOURCES",
                table: "ZPL_RESOURCES",
                column: "UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ZPL_RESOURCES",
                table: "ZPL_RESOURCES");

            migrationBuilder.RenameTable(
                name: "ZPL_RESOURCES",
                newName: "ZplResources");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZplResources",
                table: "ZplResources",
                column: "UID");
        }
    }
}
