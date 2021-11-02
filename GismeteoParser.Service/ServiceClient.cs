using GismeteoParser.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GismeteoParser.ConsoleClient
{
    public class ServiceClient : IDisposable
    {
        static HttpClient _client = new HttpClient();

        public ServiceClient(Uri serviceUri)
        {
            _client.BaseAddress = serviceUri;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<string>> GetCitiesAsync()
        {
            IEnumerable<string> cities = null;
            HttpResponseMessage response = await _client.GetAsync("api/values");
            if (response.IsSuccessStatusCode)
            {
                cities = await response.Content.ReadAsAsync<IEnumerable<string>>();
            }
            return cities;
        }

        public async Task<IEnumerable<DateTime>> GetDatesAsync(string cityName)
        {
            IEnumerable<DateTime> dates = null;
            HttpResponseMessage response = await _client.GetAsync($"api/values?cityName={cityName}");
            if (response.IsSuccessStatusCode)
            {
                dates = await response.Content.ReadAsAsync<IEnumerable<DateTime>>();
            }
            return dates;
        }

        public async Task<OneDayWeather> GetOneDayWeatherAsync(string cityName, DateTime date)
        {
            OneDayWeather oneDateWeather = null;
            HttpResponseMessage response = await _client.GetAsync($"api/values?cityName={cityName}&date={date.ToString("M")}");
            if (response.IsSuccessStatusCode)
            {
                oneDateWeather = await response.Content.ReadAsAsync<OneDayWeather>();
            }
            return oneDateWeather;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
