using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacteristicTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CHARACTERISTICS",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BUNDLE_COUNT = table.Column<short>(type: "smallint", nullable: false),
                    BOX_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PLU_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHARACTERISTICS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_CHARACTERISTICS_BOXES_BOX_UID",
                        column: x => x.BOX_UID,
                        principalTable: "BOXES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHARACTERISTICS_PLUS_PLU_UID",
                        column: x => x.PLU_UID,
                        principalTable: "PLUS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHARACTERISTICS_BOX_UID",
                table: "CHARACTERISTICS",
                column: "BOX_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_CHARACTERISTICS_UNIQ",
                table: "CHARACTERISTICS",
                columns: new[] { "PLU_UID", "BOX_UID", "BUNDLE_COUNT" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHARACTERISTICS");
        }
    }
}