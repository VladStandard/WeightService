using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehousesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WAREHOUSES",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PRODUCTION_SITE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WAREHOUSES", x => x.UID);
                    table.ForeignKey(
                        name: "FK_WAREHOUSES_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                        column: x => x.PRODUCTION_SITE_UID,
                        principalTable: "PRODUCTION_SITES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WAREHOUSES_PRODUCTION_SITE_UID",
                table: "WAREHOUSES",
                column: "PRODUCTION_SITE_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_WAREHOUSES_NAME",
                table: "WAREHOUSES",
                column: "NAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WAREHOUSES");
        }
    }
}
