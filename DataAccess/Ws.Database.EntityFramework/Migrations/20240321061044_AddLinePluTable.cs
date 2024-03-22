using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddLinePluTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LINES_PLUS_FK",
                columns: table => new
                {
                    PLU_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LINE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LINES_PLUS_FK", x => new { x.PLU_UID, x.LINE_UID });
                    table.ForeignKey(
                        name: "FK_LINES_PLUS_FK_LINES_LINE_UID",
                        column: x => x.LINE_UID,
                        principalTable: "LINES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LINES_PLUS_FK_PLUS_PLU_UID",
                        column: x => x.PLU_UID,
                        principalTable: "PLUS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LINES_PLUS_FK_LINE_UID",
                table: "LINES_PLUS_FK",
                column: "LINE_UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LINES_PLUS_FK");
        }
    }
}