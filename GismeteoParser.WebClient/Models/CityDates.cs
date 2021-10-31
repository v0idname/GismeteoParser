using System;
using System.Collections.Generic;

namespace GismeteoParser.WebClient.Models
{
    public class CityDates
    {
        public string CityName { get; set; }
        public IEnumerable<DateTime> Dates { get; set; }
    }
}
