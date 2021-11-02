using System.Collections.Generic;

namespace GismeteoParser.Grabber.Parsers
{
    public interface IPopCitiesParser
    {
        IEnumerable<string> GetPopCitiesLinks(string html);
    }
}
