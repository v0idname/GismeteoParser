using Microsoft.EntityFrameworkCore.Migrations;

namespace GismeteoParser.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitiesWeather",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitiesWeather", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OneDayWeathers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(nullable: true),
                    DayPlusMonth = table.Column<string>(nullable: true),
                    MaxTempC = table.Column<int>(nullable: false),
                    MinTempC = table.Column<int>(nullable: false),
                    MaxWindSpeedMs = table.Column<int>(nullable: false),
                    PrecipitationMm = table.Column<decimal>(nullable: false),
                    CityWeatherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneDayWeathers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneDayWeathers_CitiesWeather_CityWeatherId",
                        column: x => x.CityWeatherId,
                        principalTable: "CitiesWeather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OneDayWeathers_CityWeatherId",
                table: "OneDayWeathers",
                column: "CityWeatherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OneDayWeathers");

            migrationBuilder.DropTable(
                name: "CitiesWeather");
        }
    }
}
