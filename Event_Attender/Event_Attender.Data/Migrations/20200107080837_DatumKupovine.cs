using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Event_Attender.Data.Migrations
{
    public partial class DatumKupovine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatumKupovine",
                table: "Karta",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumKupovine",
                table: "Karta");
        }
    }
}
