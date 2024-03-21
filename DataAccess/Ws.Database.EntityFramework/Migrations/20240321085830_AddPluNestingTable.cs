using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddPluNestingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_RESOURCES_PLUS_UID",
                table: "PLUS_RESOURCES");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_RESOURCES_STORAGE_METHODS_STORAGE_METHOD_UID",
                table: "PLUS_RESOURCES");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_RESOURCES_TEMPLATES_TEMPLATE_UID",
                table: "PLUS_RESOURCES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PLUS_RESOURCES",
                table: "PLUS_RESOURCES");

            migrationBuilder.RenameTable(
                name: "PLUS_RESOURCES",
                newName: "PLUS_RESOURCES_FK");

            migrationBuilder.RenameIndex(
                name: "IX_PLUS_RESOURCES_TEMPLATE_UID",
                table: "PLUS_RESOURCES_FK",
                newName: "IX_PLUS_RESOURCES_FK_TEMPLATE_UID");

            migrationBuilder.RenameIndex(
                name: "IX_PLUS_RESOURCES_STORAGE_METHOD_UID",
                table: "PLUS_RESOURCES_FK",
                newName: "IX_PLUS_RESOURCES_FK_STORAGE_METHOD_UID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PLUS_RESOURCES_FK",
                table: "PLUS_RESOURCES_FK",
                column: "UID");

            migrationBuilder.CreateTable(
                name: "PLUS_NESTINGS_FK",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UID_1C = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BUNDLE_COUNT = table.Column<short>(type: "smallint", nullable: false),
                    BOX_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PLU_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IS_DEFAULT = table.Column<bool>(type: "bit", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLUS_NESTINGS_FK", x => x.UID);
                    table.ForeignKey(
                        name: "FK_PLUS_NESTINGS_FK_BOXES_BOX_UID",
                        column: x => x.BOX_UID,
                        principalTable: "BOXES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLUS_NESTINGS_FK_PLUS_PLU_UID",
                        column: x => x.PLU_UID,
                        principalTable: "PLUS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_NESTINGS_FK_BOX_UID",
                table: "PLUS_NESTINGS_FK",
                column: "BOX_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_PLUS_NESTINGS_FK_BUNDLE_BOX",
                table: "PLUS_NESTINGS_FK",
                columns: new[] { "BUNDLE_COUNT", "BOX_UID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PLUS_NESTINGS_FK_IS_DEFAULT_TRUE_ON_PLU",
                table: "PLUS_NESTINGS_FK",
                columns: new[] { "PLU_UID", "IS_DEFAULT" },
                unique: true,
                filter: "[IS_DEFAULT] = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_RESOURCES_FK_PLUS_UID",
                table: "PLUS_RESOURCES_FK",
                column: "UID",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_RESOURCES_FK_STORAGE_METHODS_STORAGE_METHOD_UID",
                table: "PLUS_RESOURCES_FK",
                column: "STORAGE_METHOD_UID",
                principalTable: "STORAGE_METHODS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_RESOURCES_FK_TEMPLATES_TEMPLATE_UID",
                table: "PLUS_RESOURCES_FK",
                column: "TEMPLATE_UID",
                principalTable: "TEMPLATES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_RESOURCES_FK_PLUS_UID",
                table: "PLUS_RESOURCES_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_RESOURCES_FK_STORAGE_METHODS_STORAGE_METHOD_UID",
                table: "PLUS_RESOURCES_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_RESOURCES_FK_TEMPLATES_TEMPLATE_UID",
                table: "PLUS_RESOURCES_FK");

            migrationBuilder.DropTable(
                name: "PLUS_NESTINGS_FK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PLUS_RESOURCES_FK",
                table: "PLUS_RESOURCES_FK");

            migrationBuilder.RenameTable(
                name: "PLUS_RESOURCES_FK",
                newName: "PLUS_RESOURCES");

            migrationBuilder.RenameIndex(
                name: "IX_PLUS_RESOURCES_FK_TEMPLATE_UID",
                table: "PLUS_RESOURCES",
                newName: "IX_PLUS_RESOURCES_TEMPLATE_UID");

            migrationBuilder.RenameIndex(
                name: "IX_PLUS_RESOURCES_FK_STORAGE_METHOD_UID",
                table: "PLUS_RESOURCES",
                newName: "IX_PLUS_RESOURCES_STORAGE_METHOD_UID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PLUS_RESOURCES",
                table: "PLUS_RESOURCES",
                column: "UID");

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_RESOURCES_PLUS_UID",
                table: "PLUS_RESOURCES",
                column: "UID",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_RESOURCES_STORAGE_METHODS_STORAGE_METHOD_UID",
                table: "PLUS_RESOURCES",
                column: "STORAGE_METHOD_UID",
                principalTable: "STORAGE_METHODS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_RESOURCES_TEMPLATES_TEMPLATE_UID",
                table: "PLUS_RESOURCES",
                column: "TEMPLATE_UID",
                principalTable: "TEMPLATES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
