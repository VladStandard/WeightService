using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_TemplatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_TEMPLATES_NAME_IS_WEIGHT",
                schema: "ZPL",
                table: "TEMPLATES",
                newName: "UQ_TEMPLATES__NAME__IS_WEIGHT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_TEMPLATES__NAME__IS_WEIGHT",
                schema: "ZPL",
                table: "TEMPLATES",
                newName: "UQ_TEMPLATES_NAME_IS_WEIGHT");
        }
    }
}
