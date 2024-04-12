using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Delete_Uid1C_PalletMenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                table: "PALLET_MEN");

            migrationBuilder.DropColumn(
                name: "UID_1C",
                table: "PALLET_MEN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UID_1C",
                table: "PALLET_MEN",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                table: "PALLET_MEN",
                column: "UID_1C",
                unique: true);
        }
    }
}