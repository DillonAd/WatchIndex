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

            WebDriver.Url = signInUrl;

            IWebElement submitBtn = GetLogInButton();

            do
            {
                IWebElement userNameTb = WebDriver.FindElement(By.Name("email"));
                userNameTb.SendKeys(userName);

                IWebElement passwordTb = WebDriver.FindElement(By.Name("password"));
                passwordTb.SendKeys(password);

                submitBtn.Click();

                WaitFor(() => GetLogInButton() != null, 30000);

                submitBtn = GetLogInButton();
            }
            while(submitBtn != null);

            var profiles = WebDriver.FindElements(By.ClassName("Nav__label"));

            var profile = profiles.FirstOrDefault(p => p.Text == profileName);
            profile.Click();
            
            Thread.Sleep(5000);
        }

        public override IEnumerable<string> GetListings()
        {
            const string listingUri = "https://www.hulu.com/search";

            var listings = new HashSet<string>();

            WebDriver.Url = listingUri;

            var searchField = WebDriver.FindElement(By.ClassName("cu-search-input"));

            foreach(var c in Alphabet)
            {
                Search(searchField, c.ToString());
            }

            return listings;
        }

        private IEnumerable<string> Search(in IWebElement searchField, string s)
        {
            var listings = new List<string>();
        
            searchField.Clear();
            searchField.SendKeys(s);

            var lastPageLength = 0;

            do
            {
                lastPageLength = WebDriver.PageSource.Length;

                IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriver;
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                System.Threading.Thread.Sleep(1000);

            }
            while(lastPageLength < WebDriver.PageSource.Length);

            var elements = WebDriver.FindElements(By.ClassName("ListItem__Content"))
                                    .Select(ele => ele.Text);

            foreach (var element in elements)
            {
                listings.Add(element);
            }

            if(s.Length < 2)
            {
                foreach(var c in Alphabet)
                {
                    listings.AddRange(Search(searchField, s + c.ToString()));
                }
            }

            return listings;
        }

        private IWebElement GetLogInButton() =>
                WebDriver.FindElements(By.ClassName("login-button"))
                         .FirstOrDefault(e => e.Text == "LOG IN");
    }
}