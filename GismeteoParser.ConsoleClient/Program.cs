using GismeteoParser.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GismeteoParser.ConsoleClient
{
    class Program
    {
        static HttpClient _client = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Устанавливаем подключение с сервисом...");

            _client.BaseAddress = new Uri("https://localhost:44361/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Cервис готов к работе. Доступные города:");
            var cities = GetCitiesAsync().GetAwaiter().GetResult();
            Console.WriteLine(string.Join("\r\n", cities.Select((c, i) => $"{i} - {c}")));

            Console.WriteLine("Введите название города (индекс строки): ");
            var choosenCity = Console.ReadLine();
            var cityIndex = int.Parse(choosenCity);

            Console.WriteLine("Доступные даты:");
            var dates = GetDatesAsync(cities.ElementAt(cityIndex)).GetAwaiter().GetResult();
            Console.WriteLine(string.Join("\r\n", dates.Select((d, i) => $"{i} - {d.Day}.{d.Month}")));

            Console.WriteLine("Введите дату (индекс строки): ");
            var choosenDate = Console.ReadLine();
            var dateIndex = int.Parse(choosenDate);

            var weather = GetOneDayWeather(cities.ElementAt(cityIndex), dates.ElementAt(dateIndex)).GetAwaiter().GetResult();
            Console.WriteLine(weather);

            Console.WriteLine("Нажмите кнопку для выхода из программы...");
            Console.ReadKey();
        }

        static async Task<IEnumerable<string>> GetCitiesAsync()
        {
            IEnumerable<string> cities = null;
            HttpResponseMessage response = await _client.GetAsync("api/values");
            if (response.IsSuccessStatusCode)
            {
                cities = await response.Content.ReadAsAsync<IEnumerable<string>>();
            }
            return cities;
        }

        static async Task<IEnumerable<DateTime>> GetDatesAsync(string cityName)
        {
            IEnumerable<DateTime> dates = null;
            HttpResponseMessage response = await _client.GetAsync($"api/values?cityName={cityName}");
            if (response.IsSuccessStatusCode)
            {
                dates = await response.Content.ReadAsAsync<IEnumerable<DateTime>>();
            }
            return dates;
        }

        static async Task<OneDayWeather> GetOneDayWeather(string cityName, DateTime date)
        {
            OneDayWeather oneDateWeather = null;
            HttpResponseMessage response = await _client.GetAsync($"api/values?cityName={cityName}&date={date.ToString("M")}");
            if (response.IsSuccessStatusCode)
            {
                oneDateWeather = await response.Content.ReadAsAsync<OneDayWeather>();
            }
            return oneDateWeather;
        }
    }
}
