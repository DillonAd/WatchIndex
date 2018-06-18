using HtmlAgilityPack;
using OpenQA.Selenium;
using System;

namespace WatchIndex
{
    public abstract class Aggregator : IDisposable
    {
        public abstract string ServiceKey { get; }

        protected readonly IWebDriver _webDriver;

        protected Aggregator(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public abstract void Authenticate(string userName, string password);

        protected HtmlDocument Get(string url)
        {
            _webDriver.Url = url;
            
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(_webDriver.PageSource);

            return htmlDoc;
        }

        public void Dispose()
        {
            _webDriver.Close();
            _webDriver.Dispose();
        }
    }
}