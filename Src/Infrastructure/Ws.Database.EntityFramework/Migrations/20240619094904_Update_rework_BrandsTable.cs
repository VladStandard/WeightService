using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_BrandsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_BRANDS_NAME",
                schema: "REF_1C",
                table: "BRANDS",
                newName: "UQ_BRANDS__NAME");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_BRANDS__NAME",
                schema: "REF_1C",
                table: "BRANDS",
                newName: "UQ_BRANDS_NAME");
        }
    }
}
