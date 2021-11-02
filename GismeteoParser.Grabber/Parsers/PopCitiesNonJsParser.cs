using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Grabber.Parsers
{
    /// <summary>
    /// Класс для парсинга главной страницы Gismeteo без необходимости 
    /// выполнения всех JS скриптов для выявления списка популярных городов.
    /// </summary>
    public class PopCitiesNonJsParser : IPopCitiesParser
    {
        public IEnumerable<string> GetPopCitiesLinks(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var nodes = document.DocumentNode.SelectNodes("//noscript[@id='noscript']/a");
            return nodes.Select(n => n.GetAttributeValue("href", null));
        }
    }
}
