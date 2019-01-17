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

        protected readonly IWebDriver WebDriver;
        protected readonly IList<char> Alphabet;

        protected Aggregator(string serviceKey, IWebDriver webDriver)
        {
            ServiceKey = serviceKey;

            WebDriver = webDriver;
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30.00);

            Alphabet = new List<char>();
            Alphabet.Add(' ');

            for (char c = 'a'; c <= 'z'; c++)
            {
                Alphabet.Add(c);
            }
        }

        public abstract void Authenticate(string userName, string password, string profileName);

        public abstract IEnumerable<string> GetListings();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                WebDriver.Close();
                WebDriver.Quit();
                WebDriver.Dispose();
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