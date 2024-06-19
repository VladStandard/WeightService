using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_StorageMethodsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_STORAGE_METHODS_ZPL",
                schema: "ZPL",
                table: "STORAGE_METHODS");

            migrationBuilder.RenameIndex(
                name: "UQ_STORAGE_METHODS_NAME",
                schema: "ZPL",
                table: "STORAGE_METHODS",
                newName: "UQ_STORAGE_METHODS__NAME");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_STORAGE_METHODS__NAME",
                schema: "ZPL",
                table: "STORAGE_METHODS",
                newName: "UQ_STORAGE_METHODS_NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS_ZPL",
                schema: "ZPL",
                table: "STORAGE_METHODS",
                column: "ZPL",
                unique: true);
        }
    }
}
