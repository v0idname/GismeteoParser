using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GismeteoParser.Data.Migrations
{
    public partial class Init_mysql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitiesWeather",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitiesWeather", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OneDayWeather",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                    table.PrimaryKey("PK_OneDayWeather", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneDayWeather_CitiesWeather_CityWeatherId",
                        column: x => x.CityWeatherId,
                        principalTable: "CitiesWeather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OneDayWeather_CityWeatherId",
                table: "OneDayWeather",
                column: "CityWeatherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OneDayWeather");

            migrationBuilder.DropTable(
                name: "CitiesWeather");
        }
    }
}
