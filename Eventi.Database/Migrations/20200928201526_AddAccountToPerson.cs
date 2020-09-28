using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventi.Database.Migrations
{
    public partial class AddAccountToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountID",
                table: "People",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_People_AccountID",
                table: "People",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Accounts_AccountID",
                table: "People",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Accounts_AccountID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_AccountID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "People");
        }
    }
}
