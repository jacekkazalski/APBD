using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kol2.Migrations
{
    public partial class Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Action",
                columns: new[] { "IdAction", "EndTime", "NeedSpecialEquipment", "StartTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), true, new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, new DateTime(2022, 4, 10, 0, 0, 0, 0, DateTimeKind.Local), false, new DateTime(2022, 2, 19, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, new DateTime(2021, 8, 4, 0, 0, 0, 0, DateTimeKind.Local), false, new DateTime(2021, 8, 3, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "FireTruck",
                columns: new[] { "IdFiretruck", "OperationNumber", "SpecialEquipment" },
                values: new object[,]
                {
                    { 1, "1234", false },
                    { 2, "420", true },
                    { 3, "9999", false },
                    { 4, "12455136", true }
                });

            migrationBuilder.InsertData(
                table: "FireTruck_Action",
                columns: new[] { "IdAction", "IdFiretruck", "AssignmentDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 1, new DateTime(2022, 2, 19, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 1, new DateTime(2021, 8, 3, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 1, 2, new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 2, new DateTime(2021, 8, 3, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 1, 3, new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 3, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 4, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Local) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FireTruck_Action",
                keyColumns: new[] { "IdAction", "IdFiretruck" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "FireTruck_Action",
                keyColumns: new[] { "IdAction", "IdFiretruck" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "FireTruck_Action",
                keyColumns: new[] { "IdAction", "IdFiretruck" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "FireTruck_Action",
                keyColumns: new[] { "IdAction", "IdFiretruck" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "FireTruck_Action",
                keyColumns: new[] { "IdAction", "IdFiretruck" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "FireTruck_Action",
                keyColumns: new[] { "IdAction", "IdFiretruck" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "FireTruck_Action",
                keyColumns: new[] { "IdAction", "IdFiretruck" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "FireTruck_Action",
                keyColumns: new[] { "IdAction", "IdFiretruck" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "IdAction",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "IdAction",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "IdAction",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "IdAction",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FireTruck",
                keyColumn: "IdFiretruck",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FireTruck",
                keyColumn: "IdFiretruck",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FireTruck",
                keyColumn: "IdFiretruck",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FireTruck",
                keyColumn: "IdFiretruck",
                keyValue: 4);
        }
    }
}
