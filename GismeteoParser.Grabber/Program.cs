using System;

namespace GismeteoParser.Grabber
{
    class Program
    {
        static void Main(string[] args)
        {
            var gp = new GismeteoParser();
            var w = gp.GetTopCitiesWeather();
            Console.WriteLine(string.Join("\r\n\r\n", w));
            Console.ReadKey();
        }
    }
}
