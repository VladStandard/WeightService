using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddPluResourceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DESCRIPTION",
                table: "STORAGE_METHODS",
                newName: "ZPL");

            migrationBuilder.CreateTable(
                name: "PLUS_RESOURCES",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TEMPLATE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    STORAGE_METHOD_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLUS_RESOURCES", x => x.UID);
                    table.ForeignKey(
                        name: "FK_PLUS_RESOURCES_PLUS_UID",
                        column: x => x.UID,
                        principalTable: "PLUS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLUS_RESOURCES_STORAGE_METHODS_STORAGE_METHOD_UID",
                        column: x => x.STORAGE_METHOD_UID,
                        principalTable: "STORAGE_METHODS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLUS_RESOURCES_TEMPLATES_TEMPLATE_UID",
                        column: x => x.TEMPLATE_UID,
                        principalTable: "TEMPLATES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_RESOURCES_STORAGE_METHOD_UID",
                table: "PLUS_RESOURCES",
                column: "STORAGE_METHOD_UID");

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_RESOURCES_TEMPLATE_UID",
                table: "PLUS_RESOURCES",
                column: "TEMPLATE_UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PLUS_RESOURCES");

            migrationBuilder.RenameColumn(
                name: "ZPL",
                table: "STORAGE_METHODS",
                newName: "DESCRIPTION");
        }
    }
}
