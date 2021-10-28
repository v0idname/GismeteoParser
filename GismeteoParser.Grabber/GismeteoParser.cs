using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Grabber
{
    public class GismeteoParser
    {
        const string URL = "https://www.gismeteo.ru/";
        const string TIME_RANGE_POSTFIX = "10-days/";
        const string POP_CITIES_XPATH = "//noscript[@id='noscript']";
        private HtmlWeb _htmlWeb = new HtmlWeb();

        private IEnumerable<string> GetPopCitiesLinks()
        {
            HtmlDocument document = _htmlWeb.Load(URL);
            var popCitiesNode = document.DocumentNode.SelectNodes(POP_CITIES_XPATH);
            List<string> popCitiesLinks = null;
            foreach (var citiesNode in popCitiesNode)
            {
                // делим на 2, т.к. для каждого города по 2 ноды (одна с нужными нам атрибутами,
                // другая с html-текстом)
                popCitiesLinks = new List<string>(citiesNode.ChildNodes.Count / 2);
                foreach (var cityNode in citiesNode.ChildNodes)
                {
                    foreach (var cityNodeAttr in cityNode.Attributes)
                    {
                        if (cityNodeAttr.Name == "href")
                            popCitiesLinks.Add(cityNodeAttr.Value);
                    }
                }
            }
            return popCitiesLinks?.Select(p => URL + p.TrimStart('/') + TIME_RANGE_POSTFIX).ToList();
        }

        public IEnumerable<string> Get()
        {
            return GetPopCitiesLinks();
        }
    }
}
