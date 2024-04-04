using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ВуDeleteUqIndex_NestingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_NESTINGS_BUNDLE_BOX",
                table: "NESTINGS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UQ_NESTINGS_BUNDLE_BOX",
                table: "NESTINGS",
                columns: new[] { "BUNDLE_COUNT", "BOX_UID" },
                unique: true);
        }
    }
}