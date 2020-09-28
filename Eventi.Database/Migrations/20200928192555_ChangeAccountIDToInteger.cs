using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventi.Database.Migrations
{
    public partial class ChangeAccountIDToInteger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Accounts_AccountID1",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_AccountID1",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "AccountID1",
                table: "RefreshTokens");

            migrationBuilder.AlterColumn<int>(
                name: "AccountID",
                table: "RefreshTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AccountID",
                table: "RefreshTokens",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Accounts_AccountID",
                table: "RefreshTokens",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Accounts_AccountID",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_AccountID",
                table: "RefreshTokens");

            migrationBuilder.AlterColumn<string>(
                name: "AccountID",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AccountID1",
                table: "RefreshTokens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AccountID1",
                table: "RefreshTokens",
                column: "AccountID1");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Accounts_AccountID1",
                table: "RefreshTokens",
                column: "AccountID1",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
