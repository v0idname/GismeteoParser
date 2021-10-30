using GismeteoParser.Data;

namespace GismeteoParser.Grabber.Parsers
{
    interface ICityWeatherParser
    {
        CityWeather GetCityWeather(string html);
    }
}
