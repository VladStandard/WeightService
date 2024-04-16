using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_TemplateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_TEMPLATES_NAME",
                schema: "ZPL",
                table: "TEMPLATES");

            migrationBuilder.AddColumn<short>(
                name: "HEIGHT",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<bool>(
                name: "IS_WEIGHT",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<short>(
                name: "WIDTH",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<Guid>(
                name: "TEMPLATE_UID",
                schema: "REF_1C",
                table: "PLUS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TEMPLATES_NAME_IS_WEIGHT",
                schema: "ZPL",
                table: "TEMPLATES",
                columns: new[] { "NAME", "IS_WEIGHT" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_TEMPLATES_NAME_IS_WEIGHT",
                schema: "ZPL",
                table: "TEMPLATES");

            migrationBuilder.DropColumn(
                name: "HEIGHT",
                schema: "ZPL",
                table: "TEMPLATES");

            migrationBuilder.DropColumn(
                name: "IS_WEIGHT",
                schema: "ZPL",
                table: "TEMPLATES");

            migrationBuilder.DropColumn(
                name: "WIDTH",
                schema: "ZPL",
                table: "TEMPLATES");

            migrationBuilder.DropColumn(
                name: "TEMPLATE_UID",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.CreateIndex(
                name: "UQ_TEMPLATES_NAME",
                schema: "ZPL",
                table: "TEMPLATES",
                column: "NAME",
                unique: true);
        }
    }
}
