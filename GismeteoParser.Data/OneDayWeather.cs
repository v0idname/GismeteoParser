using System;

namespace GismeteoParser.Data
{
    public class OneDayWeather : Entity
    {
        public DateTime Date { get; set; }
        public int MaxTempC { get; set; }
        public int MinTempC { get; set; }
        public int MaxWindSpeedMs { get; set; }
        public decimal PrecipitationMm { get; set; }
        
        public CityWeather CityWeather { get; set; }

        public override string ToString()
        {
            return $"{Date.Day}.{Date.Month}\t{MaxTempC} C,\t{MinTempC} C,\t{MaxWindSpeedMs} м/с,\t{PrecipitationMm} мм";
        }
    }
}
