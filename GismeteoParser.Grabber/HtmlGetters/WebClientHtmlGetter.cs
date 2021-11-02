using System.Net;

namespace GismeteoParser.Grabber.HtmlGetters
{
    class WebClientHtmlGetter : IHtmlGetter
    {
        public string GetHtmlByUrl(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                return client.DownloadString(url);
            }
        }
    }
}
