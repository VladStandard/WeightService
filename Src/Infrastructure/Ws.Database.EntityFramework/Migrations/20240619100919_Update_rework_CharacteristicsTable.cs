using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_CharacteristicsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHARACTERISTICS_BOXES_BOX_UID",
                schema: "REF_1C",
                table: "CHARACTERISTICS");

            migrationBuilder.DropForeignKey(
                name: "FK_CHARACTERISTICS_PLUS_PLU_UID",
                schema: "REF_1C",
                table: "CHARACTERISTICS");

            migrationBuilder.RenameIndex(
                name: "UQ_CHARACTERISTICS_UNIQ",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                newName: "UQ_CHARACTERISTICS__UNIQ");

            migrationBuilder.AddForeignKey(
                name: "FK_CHARACTERISTICS__BOX",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                column: "BOX_UID",
                principalSchema: "REF_1C",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CHARACTERISTICS__PLU",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHARACTERISTICS__BOX",
                schema: "REF_1C",
                table: "CHARACTERISTICS");

            migrationBuilder.DropForeignKey(
                name: "FK_CHARACTERISTICS__PLU",
                schema: "REF_1C",
                table: "CHARACTERISTICS");

            migrationBuilder.RenameIndex(
                name: "UQ_CHARACTERISTICS__UNIQ",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                newName: "UQ_CHARACTERISTICS_UNIQ");

            migrationBuilder.AddForeignKey(
                name: "FK_CHARACTERISTICS_BOXES_BOX_UID",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                column: "BOX_UID",
                principalSchema: "REF_1C",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CHARACTERISTICS_PLUS_PLU_UID",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
