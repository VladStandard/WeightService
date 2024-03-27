using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUID_1C_uniqueIndex_from_Brands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_BRANDS_UID_1C",
                table: "BRANDS");

            migrationBuilder.DropColumn(
                name: "UID_1C",
                table: "BRANDS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UID_1C",
                table: "BRANDS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "UQ_BRANDS_UID_1C",
                table: "BRANDS",
                column: "UID_1C",
                unique: true);
        }
    }
}