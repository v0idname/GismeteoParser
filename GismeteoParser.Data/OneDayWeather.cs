namespace GismeteoParser.Data
{
    public class OneDayWeather : Entity
    {
        public string DayOfWeek { get; set; }
        public string DayPlusMonth { get; set; }
        public int MaxTempC { get; set; }
        public int MinTempC { get; set; }
        public int MaxWindSpeedMs { get; set; }
        public decimal PrecipitationMm { get; set; }
    }
}
