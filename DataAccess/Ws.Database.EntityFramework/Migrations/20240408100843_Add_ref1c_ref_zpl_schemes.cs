using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Add_ref1c_ref_zpl_schemes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "REF_1C");

            migrationBuilder.EnsureSchema(
                name: "ZPL");

            migrationBuilder.EnsureSchema(
                name: "REF");

            migrationBuilder.RenameTable(
                name: "ZPL_RESOURCES",
                newName: "ZPL_RESOURCES",
                newSchema: "ZPL");

            migrationBuilder.RenameTable(
                name: "WAREHOUSES",
                newName: "WAREHOUSES",
                newSchema: "REF");

            migrationBuilder.RenameTable(
                name: "TEMPLATES",
                newName: "TEMPLATES",
                newSchema: "ZPL");

            migrationBuilder.RenameTable(
                name: "STORAGE_METHODS",
                newName: "STORAGE_METHODS",
                newSchema: "ZPL");

            migrationBuilder.RenameTable(
                name: "PRODUCTION_SITES",
                newName: "PRODUCTION_SITES",
                newSchema: "REF");

            migrationBuilder.RenameTable(
                name: "PLUS_RESOURCES",
                newName: "PLUS_RESOURCES",
                newSchema: "ZPL");

            migrationBuilder.RenameTable(
                name: "PLUS",
                newName: "PLUS",
                newSchema: "REF_1C");

            migrationBuilder.RenameTable(
                name: "NESTINGS",
                newName: "NESTINGS",
                newSchema: "REF_1C");

            migrationBuilder.RenameTable(
                name: "CLIPS",
                newName: "CLIPS",
                newSchema: "REF_1C");

            migrationBuilder.RenameTable(
                name: "CHARACTERISTICS",
                newName: "CHARACTERISTICS",
                newSchema: "REF_1C");

            migrationBuilder.RenameTable(
                name: "BUNDLES",
                newName: "BUNDLES",
                newSchema: "REF_1C");

            migrationBuilder.RenameTable(
                name: "BRANDS",
                newName: "BRANDS",
                newSchema: "REF_1C");

            migrationBuilder.RenameTable(
                name: "BOXES",
                newName: "BOXES",
                newSchema: "REF_1C");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ZPL_RESOURCES",
                schema: "ZPL",
                newName: "ZPL_RESOURCES");

            migrationBuilder.RenameTable(
                name: "WAREHOUSES",
                schema: "REF",
                newName: "WAREHOUSES");

            migrationBuilder.RenameTable(
                name: "TEMPLATES",
                schema: "ZPL",
                newName: "TEMPLATES");

            migrationBuilder.RenameTable(
                name: "STORAGE_METHODS",
                schema: "ZPL",
                newName: "STORAGE_METHODS");

            migrationBuilder.RenameTable(
                name: "PRODUCTION_SITES",
                schema: "REF",
                newName: "PRODUCTION_SITES");

            migrationBuilder.RenameTable(
                name: "PLUS_RESOURCES",
                schema: "ZPL",
                newName: "PLUS_RESOURCES");

            migrationBuilder.RenameTable(
                name: "PLUS",
                schema: "REF_1C",
                newName: "PLUS");

            migrationBuilder.RenameTable(
                name: "NESTINGS",
                schema: "REF_1C",
                newName: "NESTINGS");

            migrationBuilder.RenameTable(
                name: "CLIPS",
                schema: "REF_1C",
                newName: "CLIPS");

            migrationBuilder.RenameTable(
                name: "CHARACTERISTICS",
                schema: "REF_1C",
                newName: "CHARACTERISTICS");

            migrationBuilder.RenameTable(
                name: "BUNDLES",
                schema: "REF_1C",
                newName: "BUNDLES");

            migrationBuilder.RenameTable(
                name: "BRANDS",
                schema: "REF_1C",
                newName: "BRANDS");

            migrationBuilder.RenameTable(
                name: "BOXES",
                schema: "REF_1C",
                newName: "BOXES");
        }
    }
}
