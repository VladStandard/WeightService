using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Update_rename_UsersClaimsFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS_СLAIMS_FK");

            migrationBuilder.CreateTable(
                name: "USERS_CLAIMS_FK",
                columns: table => new
                {
                    CLAIM_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USER_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS_CLAIMS_FK", x => new { x.CLAIM_UID, x.USER_UID });
                    table.ForeignKey(
                        name: "FK_USERS_CLAIMS_FK_CLAIMS_CLAIM_UID",
                        column: x => x.CLAIM_UID,
                        principalTable: "CLAIMS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USERS_CLAIMS_FK_USERS_USER_UID",
                        column: x => x.USER_UID,
                        principalTable: "USERS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USERS_CLAIMS_FK_USER_UID",
                table: "USERS_CLAIMS_FK",
                column: "USER_UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS_CLAIMS_FK");

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
                name: "IX_USERS_СLAIMS_FK_USER_UID",
                table: "USERS_СLAIMS_FK",
                column: "USER_UID");
        }
    }
}