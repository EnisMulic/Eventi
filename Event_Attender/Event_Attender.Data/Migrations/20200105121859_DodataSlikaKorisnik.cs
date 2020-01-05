using Microsoft.EntityFrameworkCore.Migrations;

namespace Event_Attender.Data.Migrations
{
    public partial class DodataSlikaKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Korisnik",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Korisnik");
        }
    }
}
