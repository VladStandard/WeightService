using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUID_1C_uniqueIndex_from_ClipsBundlesBoxes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_CLIPS_NAME",
                table: "CLIPS");

            migrationBuilder.DropIndex(
                name: "UQ_CLIPS_UID_1C",
                table: "CLIPS");

            migrationBuilder.DropIndex(
                name: "UQ_BUNDLES_NAME",
                table: "BUNDLES");

            migrationBuilder.DropIndex(
                name: "UQ_BUNDLES_UID_1C",
                table: "BUNDLES");

            migrationBuilder.DropIndex(
                name: "UQ_BOXES_NAME",
                table: "BOXES");

            migrationBuilder.DropIndex(
                name: "UQ_BOXES_UID_1C",
                table: "BOXES");

            migrationBuilder.DropColumn(
                name: "UID_1C",
                table: "CLIPS");

            migrationBuilder.DropColumn(
                name: "UID_1C",
                table: "BUNDLES");

            migrationBuilder.DropColumn(
                name: "UID_1C",
                table: "BOXES");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UID_1C",
                table: "CLIPS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UID_1C",
                table: "BUNDLES",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UID_1C",
                table: "BOXES",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}