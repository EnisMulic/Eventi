using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventi.Database.Migrations
{
    public partial class seedMoreAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "C/7lY0n9ccMo+Oc8vD+iPaC8jUz2HfzScD7y9fmV2ruQNBs9hJrmtDN/tDFmrDxd+IqapgYS30cFJtXN4ghWOw==", "omYS2VrJh6eDKGIKk3Pjcg==" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "AccountCategory", "Email", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[,]
                {
                    { 2, 0, "admin@eventi.com", "1ih4dKxk5Tl0mA9H96s/EBEDFHctAfKLt+o3JYtRhChh6IdcH9u1auwg4PZ92CXcVejMi1aNgBL54ynCXbI7Gg==", "96XLh+bkjIPkW60Tv/je7w==", "adm" },
                    { 3, 2, "client@eventi.com", "nWNOMf1p6Ac1YUmmYq8Iyo67wz1TQZ739HrAKENFCqyBzZStj4ZdyQT2EvifLz2dii+xIeLKbJC1/CpPj9f+ag==", "KCCvLC6uXrdz0B5qyK5Dpw==", "cli" }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 19, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(1079), new DateTime(2021, 1, 18, 15, 24, 34, 401, DateTimeKind.Local).AddTicks(9333) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 19, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2559), new DateTime(2021, 1, 18, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2522) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 19, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2586), new DateTime(2021, 1, 18, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2582) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 19, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2593), new DateTime(2021, 1, 18, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 19, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2600), new DateTime(2021, 1, 18, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2597) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 19, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2610), new DateTime(2021, 1, 18, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2607) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 19, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2618), new DateTime(2021, 1, 18, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2614) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 19, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2625), new DateTime(2021, 1, 18, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2622) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 19, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2632), new DateTime(2021, 1, 18, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2629) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 19, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2641), new DateTime(2021, 1, 18, 15, 24, 34, 404, DateTimeKind.Local).AddTicks(2638) });

            migrationBuilder.UpdateData(
                table: "Organizers",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Organizer");

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "ID", "AccountID", "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { 1, 2, "Admin", "Admin", null });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "ID", "AccountID", "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { 2, 3, "Person", "Person", null });

            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "ID", "PersonID" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ID", "Address", "CreditCardNumber", "Image", "PersonID" },
                values: new object[] { 1, null, null, null, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "q0Itx91rdwBxjYgPUhVx3r2fhZ290AgJmepOoO7sEqnRxCbZEhoLQ/ghOGDToA5ZUaa6Khi2kuSNWC23cSvAFQ==", "YTd11H/Hm/uR7awLgfA1hw==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(4720), new DateTime(2021, 1, 16, 21, 0, 5, 959, DateTimeKind.Local).AddTicks(1423) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6243), new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6270), new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6266) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6277), new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6274) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6285), new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6282) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6295), new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6291) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6302), new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6299) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6310), new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6306) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6317), new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6314) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6326), new DateTime(2021, 1, 16, 21, 0, 5, 961, DateTimeKind.Local).AddTicks(6322) });

            migrationBuilder.UpdateData(
                table: "Organizers",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: null);
        }
    }
}
