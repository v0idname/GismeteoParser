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

        public override string ToString()
        {
            return $"{DayOfWeek}\t{DayPlusMonth}\t{MaxTempC} C,\t{MinTempC} C,\t{MaxWindSpeedMs} м/с,\t{PrecipitationMm} мм";
        }
    }
}
