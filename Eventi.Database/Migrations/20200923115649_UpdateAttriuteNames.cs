using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventi.Database.Migrations
{
    public partial class UpdateAttriuteNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_PurchaseTypes_KupovinaTipId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_KupovinaTipId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "KupovinaTipId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseTypeID",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PurchaseTypeID",
                table: "Tickets",
                column: "PurchaseTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_PurchaseTypes_PurchaseTypeID",
                table: "Tickets",
                column: "PurchaseTypeID",
                principalTable: "PurchaseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_PurchaseTypes_PurchaseTypeID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PurchaseTypeID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "PurchaseTypeID",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "KupovinaTipId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_KupovinaTipId",
                table: "Tickets",
                column: "KupovinaTipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_PurchaseTypes_KupovinaTipId",
                table: "Tickets",
                column: "KupovinaTipId",
                principalTable: "PurchaseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
