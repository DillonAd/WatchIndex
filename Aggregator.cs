using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WatchIndex
{
    public abstract class Aggregator : IDisposable
    {
        private readonly IWebDriver _webDriver;

        public Aggregator()
        {
            _webDriver = new ChromeDriver();
        }

        public abstract Task Authenticate(string url, string userName, string password);

        protected async Task<HtmlDocument> GetAsync(string url)
        {
            _webDriver.Url = uri;
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var result =  await response.Content.ReadAsStreamAsync();

            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(result);

            return htmlDoc;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}