using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventi.Database.Migrations
{
    public partial class UpdateDatabaseForTicketingSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Events_EventID",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Tickets_TicketID",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_PurchaseTypes_PurchaseTypeID",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "PurchaseTypes");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SaleTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PurchaseTypeID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Seats_TicketID",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PurchaseTypeID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Purchased",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "TicketID",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "EventID",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatID",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SectionID",
                table: "Seats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketID",
                table: "Purchases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTickets",
                table: "Purchases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                columns: new[] { "TicketID", "ClientID" });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    VenueID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sections_Venues_VenueID",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventID",
                table: "Tickets",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatID",
                table: "Tickets",
                column: "SeatID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SectionID",
                table: "Seats",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_VenueID",
                table: "Sections",
                column: "VenueID");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Tickets_TicketID",
                table: "Purchases",
                column: "TicketID",
                principalTable: "Tickets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Sections_SectionID",
                table: "Seats",
                column: "SectionID",
                principalTable: "Sections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Events_EventID",
                table: "Tickets",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Seats_SeatID",
                table: "Tickets",
                column: "SeatID",
                principalTable: "Seats",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Tickets_TicketID",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Sections_SectionID",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Events_EventID",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Seats_SeatID",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_EventID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SeatID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Seats_SectionID",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SeatID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SectionID",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "TicketID",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "NumberOfTickets",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseTypeID",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Purchased",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatNumber",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketID",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventID",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                columns: new[] { "EventID", "ClientID" });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    LikedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.EventID, x.ClientID });
                    table.ForeignKey(
                        name: "FK_Likes_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseClientID = table.Column<int>(type: "int", nullable: false),
                    PurchaseEventID = table.Column<int>(type: "int", nullable: false),
                    PurchaseID = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reviews_Purchases_PurchaseEventID_PurchaseClientID",
                        columns: x => new { x.PurchaseEventID, x.PurchaseClientID },
                        principalTable: "Purchases",
                        principalColumns: new[] { "EventID", "ClientID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    NumberOfTickets = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    SeatsExist = table.Column<bool>(type: "bit", nullable: false),
                    TicketCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SaleTypes_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfTickets = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    PurchaseClientID = table.Column<int>(type: "int", nullable: false),
                    PurchaseEventID = table.Column<int>(type: "int", nullable: false),
                    PurchaseID = table.Column<int>(type: "int", nullable: false),
                    SaleTypeID = table.Column<int>(type: "int", nullable: true),
                    TicketCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseTypes_SaleTypes_SaleTypeID",
                        column: x => x.SaleTypeID,
                        principalTable: "SaleTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseTypes_Purchases_PurchaseEventID_PurchaseClientID",
                        columns: x => new { x.PurchaseEventID, x.PurchaseClientID },
                        principalTable: "Purchases",
                        principalColumns: new[] { "EventID", "ClientID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PurchaseTypeID",
                table: "Tickets",
                column: "PurchaseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_TicketID",
                table: "Seats",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ClientID",
                table: "Likes",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTypes_SaleTypeID",
                table: "PurchaseTypes",
                column: "SaleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTypes_PurchaseEventID_PurchaseClientID",
                table: "PurchaseTypes",
                columns: new[] { "PurchaseEventID", "PurchaseClientID" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PurchaseEventID_PurchaseClientID",
                table: "Reviews",
                columns: new[] { "PurchaseEventID", "PurchaseClientID" });

            migrationBuilder.CreateIndex(
                name: "IX_SaleTypes_EventID",
                table: "SaleTypes",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Events_EventID",
                table: "Purchases",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Tickets_TicketID",
                table: "Seats",
                column: "TicketID",
                principalTable: "Tickets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_PurchaseTypes_PurchaseTypeID",
                table: "Tickets",
                column: "PurchaseTypeID",
                principalTable: "PurchaseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
