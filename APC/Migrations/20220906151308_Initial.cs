using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APC.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarMark = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Tachometer = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departures",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarMark = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Division = table.Column<string>(nullable: true),
                    TrackNumber = table.Column<int>(nullable: false),
                    Driver = table.Column<string>(nullable: true),
                    Senior = table.Column<string>(nullable: true),
                    Tachometer = table.Column<int>(nullable: false),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    TachometerEnd = table.Column<int>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departures", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Repair",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarMark = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Tachometer = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repair", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Seniors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seniors", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Departures");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Repair");

            migrationBuilder.DropTable(
                name: "Seniors");
        }
    }
}
