using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GismeteoParser.Data
{
    public class CityWeather : Entity
    {
        //[DataMember]
        public string CityName { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public IEnumerable<OneDayWeather> DaysWeather { get; set; }

        public override string ToString()
        {
            return $"Город {CityName}:\r\n{string.Join("\r\n", DaysWeather)}";
        }
    }
}
