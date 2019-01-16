using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WatchIndex
{
    public class HuluAggregator : Aggregator
    {
        public HuluAggregator(IWebDriver webDriver) 
            : base("Hulu", webDriver) { }

        public override void Authenticate(string userName, string password, string profileName)
        {
            const string signInUrl = "https://auth.hulu.com/web/login";

            _webDriver.Url = signInUrl;

            IWebElement submitBtn = GetLogInButton();

            do
            {
                IWebElement userNameTb = _webDriver.FindElement(By.Name("email"));
                userNameTb.SendKeys(userName);

                IWebElement passwordTb = _webDriver.FindElement(By.Name("password"));
                passwordTb.SendKeys(password);

                submitBtn.Click();

                WaitFor(() => GetLogInButton() != null, 30000);

                submitBtn = GetLogInButton();
            }
            while(submitBtn != null);

            var profiles = _webDriver.FindElements(By.ClassName("Nav__label"));

            var profile = profiles.FirstOrDefault(p => p.Text == profileName);
            profile.Click();
            
            Thread.Sleep(5000);
        }

        public override IEnumerable<string> GetListings()
        {
            const string listingUri = "https://www.hulu.com/search";

            var listings = new HashSet<string>();

            _webDriver.Url = listingUri;

            for (char c = 'a'; c <= 'z'; c++)
            {
                var lastPageLength = 0;

                do
                {
                    lastPageLength = _webDriver.PageSource.Length;

                    IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                    System.Threading.Thread.Sleep(1000);

                }
                while(lastPageLength < _webDriver.PageSource.Length);

                var elements = _webDriver.FindElements(By.ClassName("fallback-text"))
                                         .Select(ele => ele.Text);

                foreach (var element in elements)
                {
                    listings.Add(element);
                }
            }

            return listings;
        }

        private IWebElement GetLogInButton() =>
                _webDriver.FindElements(By.ClassName("login-button"))
                          .FirstOrDefault(e => e.Text == "LOG IN");
    }
}