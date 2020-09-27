using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventi.Database.Migrations
{
    public partial class renameEventSponsorPriorityFieldToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SponsorPriority",
                table: "EventSponsors");

            migrationBuilder.AddColumn<int>(
                name: "SponsorCategory",
                table: "EventSponsors",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SponsorCategory",
                table: "EventSponsors");

            migrationBuilder.AddColumn<int>(
                name: "SponsorPriority",
                table: "EventSponsors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
