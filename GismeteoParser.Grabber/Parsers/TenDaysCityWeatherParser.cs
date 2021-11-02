using GismeteoParser.Data;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GismeteoParser.Grabber.Parsers
{
    public class TenDaysCityWeatherParser : ICityWeatherParser
    {
        private const int DaysCount = 10;

        public CityWeather GetCityWeather(string html)
        {
            //var stopwatch = Stopwatch.StartNew();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var mainNode = document.DocumentNode.SelectSingleNode("//div[@class='widget__container']");
            var dates = GetDatesByNodes(mainNode);
            var temps = GetMinMaxTempC(mainNode);
            var winds = GetMaxWindSpeed(mainNode);
            var precs = GetPrecipitations(mainNode);

            //Debug.WriteLine(stopwatch.ElapsedMilliseconds);

            var oneDayWeathers = new List<OneDayWeather>(DaysCount);
            for (int i = 0; i < DaysCount; i++)
            {
                oneDayWeathers.Add(new OneDayWeather()
                {
                    Date = dates[i],
                    MinTempC = temps.min[i],
                    MaxTempC = temps.max[i],
                    MaxWindSpeedMs = winds[i],
                    PrecipitationMm = precs[i]
                });
            }

            //stopwatch.Stop();

            return new CityWeather()
            {
                CityName = GetCityName(document),
                DaysWeather = oneDayWeathers
            };
        }

        private string GetCityName(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectSingleNode("//div[@class='subnav_search_city js_citytitle']").InnerText;
        }

        private (List<int> max, List<int> min) GetMinMaxTempC(HtmlNode node)
        {
            var max = new List<int>(DaysCount);
            var min = new List<int>(DaysCount);
            var minMaxTempNodes = node.SelectSingleNode(".//div[@class='templine w_temperature']" +
                "/div[@class='chart chart__temperature']/div[@class='values']");
            foreach (var tempNode in minMaxTempNodes.ChildNodes)
            {
                var maxTempNode = tempNode.SelectSingleNode("div[@class='maxt']/span[@class='unit unit_temperature_c']");
                var minTempNode = tempNode.SelectSingleNode("div[@class='mint']/span[@class='unit unit_temperature_c']");
                if (minTempNode == null)
                    minTempNode = maxTempNode;
                max.Add(GetIntFromHtml(maxTempNode.InnerText));
                min.Add(GetIntFromHtml(minTempNode.InnerText));
            }

            return (max, min);
        }

        private int GetIntFromHtml(string htmlNumber)
        {
            int.TryParse(htmlNumber.Replace("&minus;", "-"), out int res);
            return res;
        }

        private List<int> GetMaxWindSpeed(HtmlNode node)
        {
            var windSpeeds = new List<int>(DaysCount);
            var maxWindNodes = node.SelectNodes(".//div[@class='widget__row widget__row_table widget__row_wind-or-gust']" +
                "/div[@class='widget__item']/div[@class='w_wind']/div[@class='w_wind__warning w_wind__warning_ ']" +
                "/span[@class='unit unit_wind_m_s']");
            foreach (var windNode in maxWindNodes)
            {
                windSpeeds.Add(GetIntFromHtml(windNode.InnerText));
            }
            return windSpeeds;
        }

        private List<decimal> GetPrecipitations(HtmlNode node)
        {
            var precs = new List<decimal>(DaysCount);
            var precipitationNodes = node.SelectNodes(".//div[@class='w_prec__value']");
            foreach (var precNode in precipitationNodes)
            {
                decimal.TryParse(precNode.InnerText, out decimal precMm);
                precs.Add(precMm);
            }
            return precs;
        }

        private List<DateTime> GetDatesByNodes(HtmlNode node)
        {
            var nodes = node.SelectNodes(".//div[@class='w_date']/a/span[contains(@class, 'w_date__date')]");
            var resList = new List<DateTime>(DaysCount);
            var dateNow = DateTime.Now;
            var lastMonth = dateNow.Month;
            for (int i = 0; i < DaysCount; i++)
            {
                var s = nodes[i].InnerText.Trim('\n').Trim().Split(' ');
                if (!int.TryParse(s[0], out int day))
                    day = dateNow.Day;
                if (s.Length == 2)
                    lastMonth = GetMonthByRusString(s[1]);
                resList.Add(new DateTime(dateNow.Year, lastMonth, day));
            }
            return resList;
        }

        private int GetMonthByRusString(string month)
        {
            switch (month)
            {
                case "янв": return 1;
                case "фев": return 2;
                case "мар": return 3;
                case "апр": return 4;
                case "май": return 5;
                case "июн": return 6;
                case "июл": return 7;
                case "авг": return 8;
                case "сен": return 9;
                case "окт": return 10;
                case "ноя": return 11;
                case "дек": return 12;
                default:
                    return DateTime.Now.Month;
            }
        }
    }
}
