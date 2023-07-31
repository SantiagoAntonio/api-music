using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace api_music.Migrations
{
    /// <inheritdoc />
    public partial class CDs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Members",
                type: "varchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Launch = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Poster = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDs", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CDs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Members",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldMaxLength: 120);
        }
    }
}
