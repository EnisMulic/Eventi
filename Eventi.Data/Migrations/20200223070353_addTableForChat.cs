using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventi.Data.Migrations
{
    public partial class addTableForChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatPoruke",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Poruka = table.Column<string>(nullable: true),
                    Kreirana = table.Column<DateTime>(nullable: false),
                    OsobaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatPoruke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatPoruke_Osoba_OsobaId",
                        column: x => x.OsobaId,
                        principalTable: "Osoba",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatPoruke_OsobaId",
                table: "ChatPoruke",
                column: "OsobaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatPoruke");
        }
    }
}
