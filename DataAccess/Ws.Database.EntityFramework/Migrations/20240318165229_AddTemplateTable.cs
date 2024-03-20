using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TEMPLATES",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BODY = table.Column<string>(type: "nvarchar(max)", maxLength: 10240, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATES", x => x.UID);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_TEMPLATES_NAME",
                table: "TEMPLATES",
                column: "NAME");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TEMPLATES");
        }
    }
}
