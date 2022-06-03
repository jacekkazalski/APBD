using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kol2.Migrations
{
    public partial class rebuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "IdEvent",
                keyValue: 1,
                column: "DateFrom",
                value: new DateTime(2022, 6, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "IdEvent",
                keyValue: 2,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 29, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "IdEvent",
                keyValue: 3,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2022, 6, 13, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 6, 16, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "IdEvent",
                keyValue: 1,
                column: "DateFrom",
                value: new DateTime(2022, 6, 2, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "IdEvent",
                keyValue: 2,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 28, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "IdEvent",
                keyValue: 3,
                columns: new[] { "DateFrom", "DateTo" },
                values: new object[] { new DateTime(2022, 6, 12, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
