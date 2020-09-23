using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventi.Database.Migrations
{
    public partial class changeVenueTypeToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VenueType",
                table: "Venues");

            migrationBuilder.AddColumn<int>(
                name: "VenueCategory",
                table: "Venues",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VenueCategory",
                table: "Venues");

            migrationBuilder.AddColumn<int>(
                name: "VenueType",
                table: "Venues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
