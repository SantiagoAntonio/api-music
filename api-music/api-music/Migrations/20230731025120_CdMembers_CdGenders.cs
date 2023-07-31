using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_music.Migrations
{
    /// <inheritdoc />
    public partial class CdMembers_CdGenders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CDGenders",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    CDId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDGenders", x => new { x.GenderId, x.CDId });
                    table.ForeignKey(
                        name: "FK_CDGenders_CDs_CDId",
                        column: x => x.CDId,
                        principalTable: "CDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CDGenders_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CdMembers",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    CDId = table.Column<int>(type: "int", nullable: false),
                    Responsability = table.Column<string>(type: "longtext", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CdMembers", x => new { x.MemberId, x.CDId });
                    table.ForeignKey(
                        name: "FK_CdMembers_CDs_CDId",
                        column: x => x.CDId,
                        principalTable: "CDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CdMembers_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CDGenders_CDId",
                table: "CDGenders",
                column: "CDId");

            migrationBuilder.CreateIndex(
                name: "IX_CdMembers_CDId",
                table: "CdMembers",
                column: "CDId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CDGenders");

            migrationBuilder.DropTable(
                name: "CdMembers");
        }
    }
}
