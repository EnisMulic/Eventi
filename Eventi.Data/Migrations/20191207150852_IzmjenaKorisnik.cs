using Microsoft.EntityFrameworkCore.Migrations;

namespace Event_Attender.Data.Migrations
{
    public partial class IzmjenaKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrojKreditneKartice",
                table: "Korisnik",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojKreditneKartice",
                table: "Korisnik");
        }
    }
}
