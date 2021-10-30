using Microsoft.EntityFrameworkCore.Migrations;

namespace GismeteoParser.Data.Migrations
{
    public partial class AddedRefToCityWeather : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneDayWeathers_CitiesWeather_CityWeatherId",
                table: "OneDayWeathers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OneDayWeathers",
                table: "OneDayWeathers");

            migrationBuilder.RenameTable(
                name: "OneDayWeathers",
                newName: "OneDayWeather");

            migrationBuilder.RenameIndex(
                name: "IX_OneDayWeathers_CityWeatherId",
                table: "OneDayWeather",
                newName: "IX_OneDayWeather_CityWeatherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OneDayWeather",
                table: "OneDayWeather",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OneDayWeather_CitiesWeather_CityWeatherId",
                table: "OneDayWeather",
                column: "CityWeatherId",
                principalTable: "CitiesWeather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneDayWeather_CitiesWeather_CityWeatherId",
                table: "OneDayWeather");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OneDayWeather",
                table: "OneDayWeather");

            migrationBuilder.RenameTable(
                name: "OneDayWeather",
                newName: "OneDayWeathers");

            migrationBuilder.RenameIndex(
                name: "IX_OneDayWeather_CityWeatherId",
                table: "OneDayWeathers",
                newName: "IX_OneDayWeathers_CityWeatherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OneDayWeathers",
                table: "OneDayWeathers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OneDayWeathers_CitiesWeather_CityWeatherId",
                table: "OneDayWeathers",
                column: "CityWeatherId",
                principalTable: "CitiesWeather",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
