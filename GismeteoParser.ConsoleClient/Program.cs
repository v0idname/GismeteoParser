using GismeteoParser.ServiceClient;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GismeteoParser.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Устанавливаем подключение с сервисом...");
            var serviceUri = new Uri("https://localhost:44361/");
            var client = new WeatherClient(serviceUri);

            Console.WriteLine("Cервис готов к работе. Доступные города:");
            var cities = await client.GetPopCitiesAsync();
            Console.WriteLine(string.Join("\r\n", cities.Select((c, i) => $"{i} - {c}")));

            Console.WriteLine("Введите название города (индекс строки): ");
            var choosenCity = Console.ReadLine();
            var cityIndex = int.Parse(choosenCity);

            Console.WriteLine("Доступные даты:");
            var dates = await client.GetDatesAsync(cities.ElementAt(cityIndex));
            Console.WriteLine(string.Join("\r\n", dates.Select((d, i) => $"{i} - {d.Day}.{d.Month}")));

            Console.WriteLine("Введите дату (индекс строки): ");
            var choosenDate = Console.ReadLine();
            var dateIndex = int.Parse(choosenDate);

            var weather = await client.GetOneDayWeatherAsync(cities.ElementAt(cityIndex), dates.ElementAt(dateIndex));
            Console.WriteLine(weather);

            Console.WriteLine("Нажмите кнопку для выхода из программы...");
            Console.ReadKey();
            client.Dispose();
        }
    }
}
