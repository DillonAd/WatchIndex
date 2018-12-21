using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

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

        public abstract void Authenticate(string userName, string password, string profileName);

        public abstract IEnumerable<string> GetListings();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _webDriver.Close();
                _webDriver.Quit();
                _webDriver.Dispose();
            }
        }
    }
}