using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rework_NestingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NESTINGS_BOXES_BOX_UID",
                schema: "REF_1C",
                table: "NESTINGS");

            migrationBuilder.AddForeignKey(
                name: "FK_NESTINGS__BOX",
                schema: "REF_1C",
                table: "NESTINGS",
                column: "BOX_UID",
                principalSchema: "REF_1C",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NESTINGS__BOX",
                schema: "REF_1C",
                table: "NESTINGS");

            migrationBuilder.AddForeignKey(
                name: "FK_NESTINGS_BOXES_BOX_UID",
                schema: "REF_1C",
                table: "NESTINGS",
                column: "BOX_UID",
                principalSchema: "REF_1C",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
