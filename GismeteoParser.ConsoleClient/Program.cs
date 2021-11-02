using GismeteoParser.ServiceClient;
using System;
using System.Linq;

namespace GismeteoParser.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Устанавливаем подключение с сервисом...");
            var serviceUri = new MyUri() { "https://localhost:44361/" };
            var client = new WeatherClient(serviceUri);

            Console.WriteLine("Cервис готов к работе. Доступные города:");
            var cities = client.GetPopCitiesAsync().GetAwaiter().GetResult();
            Console.WriteLine(string.Join("\r\n", cities.Select((c, i) => $"{i} - {c}")));

            Console.WriteLine("Введите название города (индекс строки): ");
            var choosenCity = Console.ReadLine();
            var cityIndex = int.Parse(choosenCity);

            Console.WriteLine("Доступные даты:");
            var dates = client.GetDatesAsync(cities.ElementAt(cityIndex)).GetAwaiter().GetResult();
            Console.WriteLine(string.Join("\r\n", dates.Select((d, i) => $"{i} - {d.Day}.{d.Month}")));

            Console.WriteLine("Введите дату (индекс строки): ");
            var choosenDate = Console.ReadLine();
            var dateIndex = int.Parse(choosenDate);

            var weather = client.GetOneDayWeatherAsync(cities.ElementAt(cityIndex), dates.ElementAt(dateIndex)).GetAwaiter().GetResult();
            Console.WriteLine(weather);

            Console.WriteLine("Нажмите кнопку для выхода из программы...");
            Console.ReadKey();
            client.Dispose();
        }
    }
}
