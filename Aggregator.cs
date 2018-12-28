using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace WatchIndex
{
    public abstract class Aggregator : IDisposable
    {
        public readonly string ServiceKey;

        protected readonly IWebDriver _webDriver;
        protected readonly ITitleStore _titleStore;

        protected Aggregator(string serviceKey, IWebDriver webDriver, ITitleStore titleStore)
        {
            ServiceKey = serviceKey;

            _webDriver = webDriver;
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30.00);

            _titleStore = titleStore;
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

                _titleStore.Dispose();
            }
        }

        protected void WaitFor(Func<bool> @event, int milliseconds)
        {
            const int waitInterval = 500;
            int waitTime = 0;

            while(waitTime >= milliseconds || @event.Invoke())
            {
                Thread.Sleep(waitInterval);
                waitTime += waitInterval;
            }
        }
    }
}