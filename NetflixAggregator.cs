using HtmlAgilityPack;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchIndex
{
    public class NetflixAggregator : Aggregator
    {
        public NetflixAggregator() : base() { }

        public override void Authenticate(string userName, string password)
        {
            const string signInUrl = "https://www.netflix.com/login";

            _webDriver.Url = signInUrl;

            IWebElement userNameTb = _webDriver.FindElement(By.Id("email"));
            userNameTb.SendKeys(userName);

            IWebElement passwordTb = _webDriver.FindElement(By.Id("password"));
            passwordTb.SendKeys(password);

            IWebElement submitBtn = _webDriver.FindElement(By.ClassName("btn-submit"));
            submitBtn.Click();
        }

        public IEnumerable<string> GetListings()
        {
            const string listingUri = "https://www.netflix.com/search";
            const string formattableQueryString = "?q={0}";

            var listings = new List<string>();
            HtmlNodeCollection results;
            HtmlDocument htmlDoc;

            for (char c = 'a'; c <= 'z'; c++)
            {
                htmlDoc = Get(listingUri + string.Format(formattableQueryString, c.ToString()));
                results = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"title-card-*-*\"]/div/a/div/div/div");

                if (results != null && results.Any())
                {
                    foreach (var node in results)
                    {
                        listings.Add(node.InnerText);
                    }
                }
            }

            return listings;
        }
    }
}
