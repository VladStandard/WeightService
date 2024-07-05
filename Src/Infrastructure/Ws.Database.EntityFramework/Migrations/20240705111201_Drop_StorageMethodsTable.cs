using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Drop_StorageMethodsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STORAGE_METHODS",
                schema: "ZPL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STORAGE_METHODS",
                schema: "ZPL",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    NAME = table.Column<string>(type: "varchar(32)", nullable: false),
                    ZPL = table.Column<string>(type: "varchar(1024)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STORAGE_METHODS", x => x.UID);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS__NAME",
                schema: "ZPL",
                table: "STORAGE_METHODS",
                column: "NAME",
                unique: true);
        }
    }
}
