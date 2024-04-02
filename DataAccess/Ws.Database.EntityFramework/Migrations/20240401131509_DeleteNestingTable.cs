using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DeleteNestingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NESTINGS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NESTINGS",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BOX_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BUNDLE_COUNT = table.Column<short>(type: "smallint", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IS_DEFAULT = table.Column<bool>(type: "bit", nullable: false),
                    PLU_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UID_1C = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NESTINGS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_NESTINGS_BOXES_BOX_UID",
                        column: x => x.BOX_UID,
                        principalTable: "BOXES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NESTINGS_BOX_UID",
                table: "NESTINGS",
                column: "BOX_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_NESTINGS_BUNDLE_BOX",
                table: "NESTINGS",
                columns: new[] { "BUNDLE_COUNT", "BOX_UID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_NESTINGS_IS_DEFAULT_TRUE_ON_PLU",
                table: "NESTINGS",
                columns: new[] { "PLU_UID", "IS_DEFAULT" },
                unique: true,
                filter: "[IS_DEFAULT] = 1");
        }
    }
}