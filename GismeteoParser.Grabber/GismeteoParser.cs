using GismeteoParser.Data;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Grabber
{
    public class GismeteoParser
    {
        const string URL = "https://www.gismeteo.ru/";
        const string TIME_RANGE_POSTFIX = "10-days/";
        const string POP_CITIES_XPATH = "//noscript[@id='noscript']";
        private HtmlWeb _htmlWeb = new HtmlWeb();

        private IEnumerable<string> GetPopCitiesLinks()
        {
            HtmlDocument document = _htmlWeb.Load(URL);
            var popCitiesNode = document.DocumentNode.SelectNodes(POP_CITIES_XPATH);
            List<string> popCitiesLinks = null;
            foreach (var citiesNode in popCitiesNode)
            {
                // делим на 2, т.к. для каждого города по 2 ноды (одна с нужными нам атрибутами,
                // другая с html-текстом)
                popCitiesLinks = new List<string>(citiesNode.ChildNodes.Count / 2);
                foreach (var cityNode in citiesNode.ChildNodes)
                {
                    foreach (var cityNodeAttr in cityNode.Attributes)
                    {
                        if (cityNodeAttr.Name == "href")
                            popCitiesLinks.Add(cityNodeAttr.Value);
                    }
                }
            }
            return popCitiesLinks?.Select(p => URL + p.TrimStart('/') + TIME_RANGE_POSTFIX).ToList();
        }

        private CityWeather GetCityWeather(string urlToCity)
        {
            HtmlDocument document = _htmlWeb.Load(urlToCity);
            var cityNode = document.DocumentNode.SelectSingleNode("//div[@class='subnav_search_city js_citytitle']");
            var daysOfWeekNodes = document.DocumentNode.SelectNodes("//div[@class='w_date']/a/div[@class='w_date__day']");
            var dayNodes = document.DocumentNode.SelectNodes("//div[@class='w_date']/a/span[contains(@class, 'w_date__date')]");
            var maxTempNodes = document.DocumentNode.SelectNodes("//div[@class='maxt']/span[@class='unit unit_temperature_c']");
            var minTempNodes = document.DocumentNode.SelectNodes("//div[@class='mint']/span[@class='unit unit_temperature_c']");
            var maxWindNodes = document.DocumentNode.SelectNodes("//div[@class='widget__row widget__row_table widget__row_wind-or-gust']/div[@class='widget__item']/div[@class='w_wind']/div[@class='w_wind__warning w_wind__warning_ ']/span[@class='unit unit_wind_m_s']");
            var precipitationNodes = document.DocumentNode.SelectNodes("//div[@class='w_prec__value']");

            var oneDayWeathers = new List<OneDayWeather>(10);
            for (int i = 0; i < 10; i++)
            {
                int.TryParse(minTempNodes[i].InnerText.Replace("&minus;", "-"), out int minTempC);
                int.TryParse(maxTempNodes[i].InnerText.Replace("&minus;", "-"), out int maxTempC);
                int.TryParse(maxWindNodes[i].InnerText.Replace("&minus;", "-"), out int MaxWindSpeedMs);
                decimal.TryParse(precipitationNodes[i].InnerText, out decimal precMm);

                oneDayWeathers.Add(new OneDayWeather()
                {
                    DayOfWeek = daysOfWeekNodes[i].InnerText,
                    DayPlusMonth = dayNodes[i].InnerText.Trim('\n').Trim(),
                    MinTempC = minTempC,
                    MaxTempC = maxTempC,
                    MaxWindSpeedMs = MaxWindSpeedMs,
                    PrecipitationMm = precMm
                });
            }

            return new CityWeather()
            { 
                CityName = cityNode.InnerText,
                DaysWeather = oneDayWeathers
            };
        }

        public IEnumerable<string> Get()
        {
            var s = GetPopCitiesLinks();
            GetCityWeather(s.First());
            return s;
        }
    }
}
