using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUID_1C_uniqueIndex_from_Plus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_NESTINGS_FK_BOXES_BOX_UID",
                table: "PLUS_NESTINGS_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_NESTINGS_FK_PLUS_PLU_UID",
                table: "PLUS_NESTINGS_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_RESOURCES_FK_PLUS_UID",
                table: "PLUS_RESOURCES_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_RESOURCES_FK_STORAGE_METHODS_STORAGE_METHOD_UID",
                table: "PLUS_RESOURCES_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_RESOURCES_FK_TEMPLATES_TEMPLATE_UID",
                table: "PLUS_RESOURCES_FK");

            migrationBuilder.DropIndex(
                name: "UQ_PLUS_UID_1C",
                table: "PLUS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PLUS_RESOURCES_FK",
                table: "PLUS_RESOURCES_FK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PLUS_NESTINGS_FK",
                table: "PLUS_NESTINGS_FK");

            migrationBuilder.DropColumn(
                name: "UID_1C",
                table: "PLUS");

            migrationBuilder.RenameTable(
                name: "PLUS_RESOURCES_FK",
                newName: "PLUS_RESOURCES");

            migrationBuilder.RenameTable(
                name: "PLUS_NESTINGS_FK",
                newName: "NESTINGS");

            migrationBuilder.RenameIndex(
                name: "IX_PLUS_RESOURCES_FK_TEMPLATE_UID",
                table: "PLUS_RESOURCES",
                newName: "IX_PLUS_RESOURCES_TEMPLATE_UID");

            migrationBuilder.RenameIndex(
                name: "IX_PLUS_RESOURCES_FK_STORAGE_METHOD_UID",
                table: "PLUS_RESOURCES",
                newName: "IX_PLUS_RESOURCES_STORAGE_METHOD_UID");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS_NESTINGS_FK_IS_DEFAULT_TRUE_ON_PLU",
                table: "NESTINGS",
                newName: "UQ_NESTINGS_IS_DEFAULT_TRUE_ON_PLU");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS_NESTINGS_FK_BUNDLE_BOX",
                table: "NESTINGS",
                newName: "UQ_NESTINGS_BUNDLE_BOX");

            migrationBuilder.RenameIndex(
                name: "IX_PLUS_NESTINGS_FK_BOX_UID",
                table: "NESTINGS",
                newName: "IX_NESTINGS_BOX_UID");

            migrationBuilder.AlterColumn<short>(
                name: "NUMBER",
                table: "PLUS",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PLUS_RESOURCES",
                table: "PLUS_RESOURCES",
                column: "UID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NESTINGS",
                table: "NESTINGS",
                column: "UID");

            migrationBuilder.AddForeignKey(
                name: "FK_NESTINGS_BOXES_BOX_UID",
                table: "NESTINGS",
                column: "BOX_UID",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NESTINGS_BOXES_BOX_UID",
                table: "NESTINGS");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_NESTINGS",
                table: "NESTINGS");

            migrationBuilder.RenameTable(
                name: "PLUS_RESOURCES",
                newName: "PLUS_RESOURCES_FK");

            migrationBuilder.RenameTable(
                name: "NESTINGS",
                newName: "PLUS_NESTINGS_FK");

            migrationBuilder.RenameIndex(
                name: "IX_PLUS_RESOURCES_TEMPLATE_UID",
                table: "PLUS_RESOURCES_FK",
                newName: "IX_PLUS_RESOURCES_FK_TEMPLATE_UID");

            migrationBuilder.RenameIndex(
                name: "IX_PLUS_RESOURCES_STORAGE_METHOD_UID",
                table: "PLUS_RESOURCES_FK",
                newName: "IX_PLUS_RESOURCES_FK_STORAGE_METHOD_UID");

            migrationBuilder.RenameIndex(
                name: "UQ_NESTINGS_IS_DEFAULT_TRUE_ON_PLU",
                table: "PLUS_NESTINGS_FK",
                newName: "UQ_PLUS_NESTINGS_FK_IS_DEFAULT_TRUE_ON_PLU");

            migrationBuilder.RenameIndex(
                name: "UQ_NESTINGS_BUNDLE_BOX",
                table: "PLUS_NESTINGS_FK",
                newName: "UQ_PLUS_NESTINGS_FK_BUNDLE_BOX");

            migrationBuilder.RenameIndex(
                name: "IX_NESTINGS_BOX_UID",
                table: "PLUS_NESTINGS_FK",
                newName: "IX_PLUS_NESTINGS_FK_BOX_UID");

            migrationBuilder.AlterColumn<int>(
                name: "NUMBER",
                table: "PLUS",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<Guid>(
                name: "UID_1C",
                table: "PLUS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PLUS_RESOURCES_FK",
                table: "PLUS_RESOURCES_FK",
                column: "UID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PLUS_NESTINGS_FK",
                table: "PLUS_NESTINGS_FK",
                column: "UID");

            migrationBuilder.CreateIndex(
                name: "UQ_PLUS_UID_1C",
                table: "PLUS",
                column: "UID_1C",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_NESTINGS_FK_BOXES_BOX_UID",
                table: "PLUS_NESTINGS_FK",
                column: "BOX_UID",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_NESTINGS_FK_PLUS_PLU_UID",
                table: "PLUS_NESTINGS_FK",
                column: "PLU_UID",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

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
    }
}