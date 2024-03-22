using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddPluTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PLUS",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UID_1C = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FULL_NAME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NUMBER = table.Column<int>(type: "int", nullable: false),
                    SHELF_LIFE_DAYS = table.Column<byte>(type: "tinyint", nullable: false),
                    EAN_13 = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    ITF_14 = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    IS_WEIGHT = table.Column<bool>(type: "bit", nullable: false),
                    BUNDLE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BRAND_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CLIP_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLUS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_PLUS_BRANDS_BRAND_UID",
                        column: x => x.BRAND_UID,
                        principalTable: "BRANDS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLUS_BUNDLES_BUNDLE_UID",
                        column: x => x.BUNDLE_UID,
                        principalTable: "BUNDLES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLUS_CLIPS_CLIP_UID",
                        column: x => x.CLIP_UID,
                        principalTable: "CLIPS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_BRAND_UID",
                table: "PLUS",
                column: "BRAND_UID");

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_BUNDLE_UID",
                table: "PLUS",
                column: "BUNDLE_UID");

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_CLIP_UID",
                table: "PLUS",
                column: "CLIP_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_LINES_NUMBER",
                table: "PLUS",
                column: "NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PLUS_UID_1C",
                table: "PLUS",
                column: "UID_1C",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PLUS");
        }
    }
}