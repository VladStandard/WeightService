using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Add_StorageMethod_PluTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PLUS_RESOURCES",
                schema: "ZPL");

            migrationBuilder.AlterColumn<string>(
                name: "ITF_14",
                schema: "REF_1C",
                table: "PLUS",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(144)",
                oldMaxLength: 144);

            migrationBuilder.AlterColumn<string>(
                name: "EAN_13",
                schema: "REF_1C",
                table: "PLUS",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(133)",
                oldMaxLength: 133);

            migrationBuilder.AddColumn<string>(
                name: "STORAGE_METHOD",
                schema: "REF_1C",
                table: "PLUS",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STORAGE_METHOD",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.AlterColumn<string>(
                name: "ITF_14",
                schema: "REF_1C",
                table: "PLUS",
                type: "varchar(144)",
                maxLength: 144,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "EAN_13",
                schema: "REF_1C",
                table: "PLUS",
                type: "varchar(133)",
                maxLength: 133,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(13)",
                oldMaxLength: 13);

            migrationBuilder.CreateTable(
                name: "PLUS_RESOURCES",
                schema: "ZPL",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    STORAGE_METHOD_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TEMPLATE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLUS_RESOURCES", x => x.UID);
                    table.ForeignKey(
                        name: "FK_PLUS_RESOURCES_PLUS_UID",
                        column: x => x.UID,
                        principalSchema: "REF_1C",
                        principalTable: "PLUS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLUS_RESOURCES_STORAGE_METHODS_STORAGE_METHOD_UID",
                        column: x => x.STORAGE_METHOD_UID,
                        principalSchema: "ZPL",
                        principalTable: "STORAGE_METHODS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLUS_RESOURCES_TEMPLATES_TEMPLATE_UID",
                        column: x => x.TEMPLATE_UID,
                        principalSchema: "ZPL",
                        principalTable: "TEMPLATES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_RESOURCES_STORAGE_METHOD_UID",
                schema: "ZPL",
                table: "PLUS_RESOURCES",
                column: "STORAGE_METHOD_UID");

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_RESOURCES_TEMPLATE_UID",
                schema: "ZPL",
                table: "PLUS_RESOURCES",
                column: "TEMPLATE_UID");
        }
    }
}
