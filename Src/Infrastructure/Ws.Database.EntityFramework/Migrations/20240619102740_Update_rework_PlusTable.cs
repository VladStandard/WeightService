using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_PlusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_BRANDS_BRAND_UID",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_BUNDLES_BUNDLE_UID",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_CLIPS_CLIP_UID",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS_NUMBER",
                schema: "REF_1C",
                table: "PLUS",
                newName: "UQ_PLUS__NUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_TEMPLATE_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "TEMPLATE_UID");

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__BRAND",
                schema: "REF_1C",
                table: "PLUS",
                column: "BRAND_UID",
                principalSchema: "REF_1C",
                principalTable: "BRANDS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__BUNDLE",
                schema: "REF_1C",
                table: "PLUS",
                column: "BUNDLE_UID",
                principalSchema: "REF_1C",
                principalTable: "BUNDLES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__CLIP",
                schema: "REF_1C",
                table: "PLUS",
                column: "CLIP_UID",
                principalSchema: "REF_1C",
                principalTable: "CLIPS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__TEMPLATE",
                schema: "REF_1C",
                table: "PLUS",
                column: "TEMPLATE_UID",
                principalSchema: "ZPL",
                principalTable: "TEMPLATES",
                principalColumn: "UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__BRAND",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__BUNDLE",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__CLIP",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__TEMPLATE",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropIndex(
                name: "IX_PLUS_TEMPLATE_UID",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS__NUMBER",
                schema: "REF_1C",
                table: "PLUS",
                newName: "UQ_PLUS_NUMBER");

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_BRANDS_BRAND_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "BRAND_UID",
                principalSchema: "REF_1C",
                principalTable: "BRANDS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_BUNDLES_BUNDLE_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "BUNDLE_UID",
                principalSchema: "REF_1C",
                principalTable: "BUNDLES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_CLIPS_CLIP_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "CLIP_UID",
                principalSchema: "REF_1C",
                principalTable: "CLIPS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
