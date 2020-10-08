using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventi.Database.Migrations
{
    public partial class seedEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Organizers",
                newName: "ID");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "AccountCategory", "Email", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, 1, "org@eventi.com", "q0Itx91rdwBxjYgPUhVx3r2fhZ290AgJmepOoO7sEqnRxCbZEhoLQ/ghOGDToA5ZUaa6Khi2kuSNWC23cSvAFQ==", "YTd11H/Hm/uR7awLgfA1hw==", "org" });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "ID", "Address", "CityID", "Name", "VenueCategory" },
                values: new object[,]
                {
                    { 1, "Address", 1, "Venue 01", 1 },
                    { 2, "Address", 1, "Venue 02", 1 },
                    { 3, "Address", 1, "Venue 03", 1 },
                    { 4, "Address", 1, "Venue 04", 1 },
                    { 5, "Address", 1, "Venue 05", 0 },
                    { 6, "Address", 1, "Venue 06", 0 },
                    { 7, "Address", 2, "Venue 07", 0 },
                    { 8, "Address", 2, "Venue 08", 2 },
                    { 9, "Address", 3, "Venue 09", 2 },
                    { 10, "Address", 3, "Venue 10", 2 }
                });

            migrationBuilder.InsertData(
                table: "Organizers",
                columns: new[] { "ID", "AccountID", "CityID", "Name", "PhoneNumber" },
                values: new object[] { 1, 1, null, null, null });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "ID", "AdministratorID", "Description", "End", "EventCategory", "Image", "IsApproved", "IsCanceled", "Name", "OrganizerID", "Start", "VenueID" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(4720), 0, null, true, false, "Event 01", 1, new DateTime(2021, 1, 16, 21, 0, 5, 959, DateTimeKind.Local).AddTicks(1423), 1 },
                    { 2, null, null, new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6243), 0, null, true, false, "Event 01", 1, new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6209), 2 },
                    { 3, null, null, new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6270), 0, null, true, false, "Event 01", 1, new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6266), 3 },
                    { 4, null, null, new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6277), 0, null, true, false, "Event 01", 1, new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6274), 4 },
                    { 5, null, null, new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6285), 0, null, true, false, "Event 01", 1, new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6282), 5 },
                    { 6, null, null, new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6295), 0, null, true, false, "Event 01", 1, new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6291), 6 },
                    { 7, null, null, new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6302), 0, null, true, false, "Event 01", 1, new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6299), 7 },
                    { 8, null, null, new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6310), 0, null, true, false, "Event 01", 1, new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6306), 8 },
                    { 9, null, null, new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6317), 0, null, true, false, "Event 01", 1, new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6314), 9 },
                    { 10, null, null, new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6326), 0, null, true, false, "Event 01", 1, new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6322), 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Organizers",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Organizers",
                newName: "Id");
        }
    }
}
