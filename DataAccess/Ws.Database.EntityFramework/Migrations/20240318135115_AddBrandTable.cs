using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "BRANDS",
            columns: table => new
            {
                UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                UID_1C = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BRANDS", x => x.UID);
            });

            migrationBuilder.CreateIndex(
            name: "UQ_BRANDS_NAME",
            table: "BRANDS",
            column: "NAME");

            migrationBuilder.CreateIndex(
            name: "UQ_BRANDS_UID_1C",
            table: "BRANDS",
            column: "UID_1C");

            migrationBuilder.CreateIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                table: "PALLET_MEN",
                column: "UID_1C");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                table: "PALLET_MEN");

            migrationBuilder.DropTable(
                name: "BRANDS");
        }
    }
}