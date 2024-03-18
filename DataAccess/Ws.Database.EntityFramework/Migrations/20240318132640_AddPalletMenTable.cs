using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddPalletMenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PALLET_MEN",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UID_1C = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    SURNAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PATRONYMIC = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PALLET_MEN", x => x.UID);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_PALLET_MEN_FIO",
                table: "PALLET_MEN",
                columns: new[] { "NAME", "SURNAME", "PATRONYMIC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PALLET_MEN");
        }
    }
}
