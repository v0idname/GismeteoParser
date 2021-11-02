using GismeteoParser.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GismeteoParser.ServiceClient
{
    public interface IWeatherClient : IDisposable
    {
        Task<IEnumerable<string>> GetPopCitiesAsync();

        Task<IEnumerable<DateTime>> GetDatesAsync(string cityName);

        Task<OneDayWeather> GetOneDayWeatherAsync(string cityName, DateTime date);
    }
}
