using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddProductionSitesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRODUCTION_SITES",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTION_SITES", x => x.UID);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_PRODUCTION_SITES_ADDRESS",
                table: "PRODUCTION_SITES",
                column: "ADDRESS");

            migrationBuilder.CreateIndex(
                name: "UQ_PRODUCTION_SITES_NAME",
                table: "PRODUCTION_SITES",
                column: "NAME");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUCTION_SITES");
        }
    }
}
