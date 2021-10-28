using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Grabber
{
    class Program
    {
        static void Main(string[] args)
        {
            var gp = new GismeteoParser();
            var p = gp.Get();
        }
    }
}
