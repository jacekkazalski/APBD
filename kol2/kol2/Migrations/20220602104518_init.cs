using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kol2.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    IdEvent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Event_pk", x => x.IdEvent);
                });

            migrationBuilder.CreateTable(
                name: "Organiser",
                columns: table => new
                {
                    IdOrganiser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Organiser_pk", x => x.IdOrganiser);
                });

            migrationBuilder.CreateTable(
                name: "Event_Organiser",
                columns: table => new
                {
                    IdEvent = table.Column<int>(type: "int", nullable: false),
                    IdOrganiser = table.Column<int>(type: "int", nullable: false),
                    MainOrganiser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EventOrganiser_pk", x => new { x.IdEvent, x.IdOrganiser });
                    table.ForeignKey(
                        name: "EventOrganiser_Event",
                        column: x => x.IdEvent,
                        principalTable: "Event",
                        principalColumn: "IdEvent",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "EventOrganiser_Organiser",
                        column: x => x.IdOrganiser,
                        principalTable: "Organiser",
                        principalColumn: "IdOrganiser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "IdEvent", "DateFrom", "DateTo", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "super festiwal" },
                    { 2, new DateTime(2022, 5, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 28, 0, 0, 0, 0, DateTimeKind.Local), "skonczony festiwal" },
                    { 3, new DateTime(2022, 6, 12, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Local), "festiwal z przyszlosci" }
                });

            migrationBuilder.InsertData(
                table: "Organiser",
                columns: new[] { "IdOrganiser", "Name" },
                values: new object[,]
                {
                    { 1, "duzy organizator" },
                    { 2, "sredni organizator" },
                    { 3, "maly organizator" }
                });

            migrationBuilder.InsertData(
                table: "Event_Organiser",
                columns: new[] { "IdEvent", "IdOrganiser", "MainOrganiser" },
                values: new object[,]
                {
                    { 1, 1, true },
                    { 2, 2, true },
                    { 3, 2, true },
                    { 2, 3, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_Organiser_IdOrganiser",
                table: "Event_Organiser",
                column: "IdOrganiser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event_Organiser");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Organiser");
        }
    }
}
