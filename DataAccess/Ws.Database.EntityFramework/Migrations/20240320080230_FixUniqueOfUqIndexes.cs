using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FixUniqueOfUqIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_TEMPLATES_NAME",
                table: "TEMPLATES");

            migrationBuilder.DropIndex(
                name: "UQ_STORAGE_METHODS_NAME",
                table: "STORAGE_METHODS");

            migrationBuilder.DropIndex(
                name: "UQ_STORAGE_METHODS_ZPL",
                table: "STORAGE_METHODS");

            migrationBuilder.DropIndex(
                name: "UQ_PRODUCTION_SITES_ADDRESS",
                table: "PRODUCTION_SITES");

            migrationBuilder.DropIndex(
                name: "UQ_PRODUCTION_SITES_NAME",
                table: "PRODUCTION_SITES");

            migrationBuilder.DropIndex(
                name: "UQ_PALLET_MEN_FIO",
                table: "PALLET_MEN");

            migrationBuilder.DropIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                table: "PALLET_MEN");

            migrationBuilder.DropIndex(
                name: "UQ_CLIPS_NAME",
                table: "CLIPS");

            migrationBuilder.DropIndex(
                name: "UQ_CLIPS_UID_1C",
                table: "CLIPS");

            migrationBuilder.DropIndex(
                name: "UQ_CLAIMS_NAME",
                table: "CLAIMS");

            migrationBuilder.DropIndex(
                name: "UQ_BUNDLES_NAME",
                table: "BUNDLES");

            migrationBuilder.DropIndex(
                name: "UQ_BUNDLES_UID_1C",
                table: "BUNDLES");

            migrationBuilder.DropIndex(
                name: "UQ_BRANDS_NAME",
                table: "BRANDS");

            migrationBuilder.DropIndex(
                name: "UQ_BRANDS_UID_1C",
                table: "BRANDS");

            migrationBuilder.DropIndex(
                name: "UQ_BOXES_NAME",
                table: "BOXES");

            migrationBuilder.DropIndex(
                name: "UQ_BOXES_UID_1C",
                table: "BOXES");

            migrationBuilder.CreateIndex(
                name: "UQ_TEMPLATES_NAME",
                table: "TEMPLATES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS_NAME",
                table: "STORAGE_METHODS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS_ZPL",
                table: "STORAGE_METHODS",
                column: "DESCRIPTION",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PRODUCTION_SITES_ADDRESS",
                table: "PRODUCTION_SITES",
                column: "ADDRESS",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PRODUCTION_SITES_NAME",
                table: "PRODUCTION_SITES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PALLET_MEN_FIO",
                table: "PALLET_MEN",
                columns: new[] { "NAME", "SURNAME", "PATRONYMIC" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                table: "PALLET_MEN",
                column: "UID_1C",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_CLIPS_NAME",
                table: "CLIPS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_CLIPS_UID_1C",
                table: "CLIPS",
                column: "UID_1C",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_CLAIMS_NAME",
                table: "CLAIMS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_BUNDLES_NAME",
                table: "BUNDLES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_BUNDLES_UID_1C",
                table: "BUNDLES",
                column: "UID_1C",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_BRANDS_NAME",
                table: "BRANDS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_BRANDS_UID_1C",
                table: "BRANDS",
                column: "UID_1C",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_BOXES_NAME",
                table: "BOXES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_BOXES_UID_1C",
                table: "BOXES",
                column: "UID_1C",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_TEMPLATES_NAME",
                table: "TEMPLATES");

            migrationBuilder.DropIndex(
                name: "UQ_STORAGE_METHODS_NAME",
                table: "STORAGE_METHODS");

            migrationBuilder.DropIndex(
                name: "UQ_STORAGE_METHODS_ZPL",
                table: "STORAGE_METHODS");

            migrationBuilder.DropIndex(
                name: "UQ_PRODUCTION_SITES_ADDRESS",
                table: "PRODUCTION_SITES");

            migrationBuilder.DropIndex(
                name: "UQ_PRODUCTION_SITES_NAME",
                table: "PRODUCTION_SITES");

            migrationBuilder.DropIndex(
                name: "UQ_PALLET_MEN_FIO",
                table: "PALLET_MEN");

            migrationBuilder.DropIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                table: "PALLET_MEN");

            migrationBuilder.DropIndex(
                name: "UQ_CLIPS_NAME",
                table: "CLIPS");

            migrationBuilder.DropIndex(
                name: "UQ_CLIPS_UID_1C",
                table: "CLIPS");

            migrationBuilder.DropIndex(
                name: "UQ_CLAIMS_NAME",
                table: "CLAIMS");

            migrationBuilder.DropIndex(
                name: "UQ_BUNDLES_NAME",
                table: "BUNDLES");

            migrationBuilder.DropIndex(
                name: "UQ_BUNDLES_UID_1C",
                table: "BUNDLES");

            migrationBuilder.DropIndex(
                name: "UQ_BRANDS_NAME",
                table: "BRANDS");

            migrationBuilder.DropIndex(
                name: "UQ_BRANDS_UID_1C",
                table: "BRANDS");

            migrationBuilder.DropIndex(
                name: "UQ_BOXES_NAME",
                table: "BOXES");

            migrationBuilder.DropIndex(
                name: "UQ_BOXES_UID_1C",
                table: "BOXES");

            migrationBuilder.CreateIndex(
                name: "UQ_TEMPLATES_NAME",
                table: "TEMPLATES",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS_NAME",
                table: "STORAGE_METHODS",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS_ZPL",
                table: "STORAGE_METHODS",
                column: "DESCRIPTION");

            migrationBuilder.CreateIndex(
                name: "UQ_PRODUCTION_SITES_ADDRESS",
                table: "PRODUCTION_SITES",
                column: "ADDRESS");

            migrationBuilder.CreateIndex(
                name: "UQ_PRODUCTION_SITES_NAME",
                table: "PRODUCTION_SITES",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_PALLET_MEN_FIO",
                table: "PALLET_MEN",
                columns: new[] { "NAME", "SURNAME", "PATRONYMIC" });

            migrationBuilder.CreateIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                table: "PALLET_MEN",
                column: "UID_1C");

            migrationBuilder.CreateIndex(
                name: "UQ_CLIPS_NAME",
                table: "CLIPS",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_CLIPS_UID_1C",
                table: "CLIPS",
                column: "UID_1C");

            migrationBuilder.CreateIndex(
                name: "UQ_CLAIMS_NAME",
                table: "CLAIMS",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_BUNDLES_NAME",
                table: "BUNDLES",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_BUNDLES_UID_1C",
                table: "BUNDLES",
                column: "UID_1C");

            migrationBuilder.CreateIndex(
                name: "UQ_BRANDS_NAME",
                table: "BRANDS",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_BRANDS_UID_1C",
                table: "BRANDS",
                column: "UID_1C");

            migrationBuilder.CreateIndex(
                name: "UQ_BOXES_NAME",
                table: "BOXES",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_BOXES_UID_1C",
                table: "BOXES",
                column: "UID_1C");
        }
    }
}
