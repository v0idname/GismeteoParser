using GismeteoParser.Service;
using System;
using System.Linq;

namespace GismeteoParser.ConsoleClient
{
    class Program
    {
        const string _mySqlConnString = "server=localhost;user=root;password=root;database=GismeteoParser.db;";
        const string _mySqlServerVersion = "5.7.36";

        static void Main(string[] args)
        {
            Console.WriteLine("Устанавливаем подключение с сервисом...");
            var service = new GismeteoDataService();
            service.ConnectToMySql(_mySqlConnString, _mySqlServerVersion);

            Console.WriteLine("Cервис готов к работе. Доступные города:");
            var cities = service.GetCities();
            Console.WriteLine(string.Join("\r\n", cities.Select((c, i) => $"{i} - {c}")));

            Console.WriteLine("Введите название города (индекс строки): ");
            var choosenCity = Console.ReadLine();
            var cityIndex = int.Parse(choosenCity);

            Console.WriteLine("Доступные даты:");
            var dates = service.GetDates(cities.ElementAt(cityIndex));
            Console.WriteLine(string.Join("\r\n", dates.Select((d, i) => $"{i} - {d.Day}.{d.Month}")));

            Console.WriteLine("Введите дату (индекс строки): ");
            var choosenDate = Console.ReadLine();
            var dateIndex = int.Parse(choosenDate);

            Console.WriteLine(service.GetOneDayWeather(cities.ElementAt(cityIndex), dates.ElementAt(dateIndex)));

            Console.WriteLine("Нажмите кнопку для выхода из программы...");
            Console.ReadKey();
            service.Dispose();
        }
    }
}
