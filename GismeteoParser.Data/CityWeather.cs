﻿using System.Collections.Generic;

namespace GismeteoParser.Data
{
    public class CityWeather
    {
        public string CityName { get; set; }

        public IEnumerable<OneDayWeather> DaysWeather { get; set; }
    }
}