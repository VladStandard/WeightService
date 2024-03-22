using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddPrintersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRINTERS",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    IP_V4 = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    TYPE = table.Column<string>(type: "varchar(8)", nullable: false),
                    PRODUCTION_SITE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRINTERS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_PRINTERS_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                        column: x => x.PRODUCTION_SITE_UID,
                        principalTable: "PRODUCTION_SITES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRINTERS_PRODUCTION_SITE_UID",
                table: "PRINTERS",
                column: "PRODUCTION_SITE_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_PRINTERS_IP_V4",
                table: "PRINTERS",
                column: "IP_V4",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PRINTERS_NAME",
                table: "PRINTERS",
                column: "NAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRINTERS");
        }
    }
}