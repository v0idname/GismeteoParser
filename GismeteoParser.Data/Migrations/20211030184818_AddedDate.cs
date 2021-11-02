using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GismeteoParser.Data.Migrations
{
    public partial class AddedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "OneDayWeather");

            migrationBuilder.DropColumn(
                name: "DayPlusMonth",
                table: "OneDayWeather");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "OneDayWeather",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "OneDayWeather");

            migrationBuilder.AddColumn<string>(
                name: "DayOfWeek",
                table: "OneDayWeather",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DayPlusMonth",
                table: "OneDayWeather",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
