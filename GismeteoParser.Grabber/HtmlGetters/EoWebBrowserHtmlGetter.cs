using EO.WebBrowser;

namespace GismeteoParser.Grabber.HtmlGetters
{
    class EoWebBrowserHtmlGetter : IHtmlGetter
    {
        public string GetHtmlByUrl(string url)
        {
            string html = string.Empty;
            using (ThreadRunner threadRunner = new ThreadRunner())
            {
                using (WebView webView = threadRunner.CreateWebView())
                { 
                    threadRunner.Send(() =>
                    {
                        webView.LoadUrlAndWait(url);
                        html = webView.GetHtml();
                    });
                }
            }
            return html;
        }
    }
}
