﻿using GismeteoParser.Data;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GismeteoParser.Grabber.Parsers
{
    public class TenDaysCityWeatherParser : ICityWeatherParser
    {
        public CityWeather GetCityWeather(string html)
        {
            //var stopwatch = Stopwatch.StartNew();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var cityNode = document.DocumentNode.SelectSingleNode("//div[@class='subnav_search_city js_citytitle']");
            var dayNodes = document.DocumentNode.SelectNodes("//div[@class='w_date']/a/span[contains(@class, 'w_date__date')]");
            var minMaxTempNodes = document.DocumentNode.SelectSingleNode("//div[@class='templine w_temperature']/div[@class='chart chart__temperature']/div[@class='values']");
            var maxWindNodes = document.DocumentNode.SelectNodes("//div[@class='widget__row widget__row_table widget__row_wind-or-gust']/div[@class='widget__item']/div[@class='w_wind']/div[@class='w_wind__warning w_wind__warning_ ']/span[@class='unit unit_wind_m_s']");
            var precipitationNodes = document.DocumentNode.SelectNodes("//div[@class='w_prec__value']");

            var dates = GetDatesByNodes(dayNodes);

            //Debug.WriteLine(stopwatch.ElapsedMilliseconds);

            var oneDayWeathers = new List<OneDayWeather>(10);
            for (int i = 0; i < 10; i++)
            {
                var maxTempNode = minMaxTempNodes.ChildNodes[i].SelectSingleNode("div[@class='maxt']/span[@class='unit unit_temperature_c']");
                var minTempNode = minMaxTempNodes.ChildNodes[i].SelectSingleNode("div[@class='mint']/span[@class='unit unit_temperature_c']");
                if (minTempNode == null)
                    minTempNode = maxTempNode;
                int.TryParse(minTempNode.InnerText.Replace("&minus;", "-"), out int minTempC);
                int.TryParse(maxTempNode.InnerText.Replace("&minus;", "-"), out int maxTempC);
                int.TryParse(maxWindNodes[i].InnerText.Replace("&minus;", "-"), out int MaxWindSpeedMs);
                decimal.TryParse(precipitationNodes[i].InnerText, out decimal precMm);

                //Debug.WriteLine(stopwatch.ElapsedMilliseconds);

                oneDayWeathers.Add(new OneDayWeather()
                {
                    Date = dates[i],
                    MinTempC = minTempC,
                    MaxTempC = maxTempC,
                    MaxWindSpeedMs = MaxWindSpeedMs,
                    PrecipitationMm = precMm
                });
            }

            //stopwatch.Stop();

            return new CityWeather()
            {
                CityName = cityNode.InnerText,
                DaysWeather = oneDayWeathers
            };
        }

        private List<DateTime> GetDatesByNodes(HtmlNodeCollection nodes)
        {
            var resList = new List<DateTime>(10);
            var dateNow = DateTime.Now;
            var lastMonth = dateNow.Month;
            for (int i = 0; i < 10; i++)
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