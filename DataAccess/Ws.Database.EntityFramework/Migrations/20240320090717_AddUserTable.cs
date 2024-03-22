using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PRODUCTION_SITE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LOGIN_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_USERS_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                        column: x => x.PRODUCTION_SITE_UID,
                        principalTable: "PRODUCTION_SITES",
                        principalColumn: "UID");
                });

            migrationBuilder.CreateTable(
                name: "USERS_СLAIMS_FK",
                columns: table => new
                {
                    CLAIM_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USER_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS_СLAIMS_FK", x => new { x.CLAIM_UID, x.USER_UID });
                    table.ForeignKey(
                        name: "FK_USERS_СLAIMS_FK_CLAIMS_CLAIM_UID",
                        column: x => x.CLAIM_UID,
                        principalTable: "CLAIMS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USERS_СLAIMS_FK_USERS_USER_UID",
                        column: x => x.USER_UID,
                        principalTable: "USERS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USERS_PRODUCTION_SITE_UID",
                table: "USERS",
                column: "PRODUCTION_SITE_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_USERS_NAME",
                table: "USERS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_СLAIMS_FK_USER_UID",
                table: "USERS_СLAIMS_FK",
                column: "USER_UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS_СLAIMS_FK");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}