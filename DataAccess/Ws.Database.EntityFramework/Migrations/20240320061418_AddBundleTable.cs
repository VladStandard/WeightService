using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddBundlesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BUNDLES",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    WEIGHT = table.Column<decimal>(type: "decimal(4,3)", nullable: false),
                    UID_1C = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BUNDLES", x => x.UID);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_BUNDLES_NAME",
                table: "BUNDLES",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "UQ_BUNDLES_UID_1C",
                table: "BUNDLES",
                column: "UID_1C");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BUNDLES");
        }
    }
}