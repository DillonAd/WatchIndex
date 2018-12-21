using HtmlAgilityPack;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchIndex
{
    public class NetflixAggregator : Aggregator
    {
        public override string ServiceKey { get; }

        public NetflixAggregator(IWebDriver webDriver) : base(webDriver) 
        { 
            ServiceKey = "Netflix";
        }

        public override void Authenticate(string userName, string password, string profileName)
        {
            const string signInUrl = "https://www.netflix.com/login";

            _webDriver.Url = signInUrl;

            IWebElement userNameTb = _webDriver.FindElement(By.Id("id_userLoginId"));
            userNameTb.SendKeys(userName);

            IWebElement passwordTb = _webDriver.FindElement(By.Id("id_password"));
            passwordTb.SendKeys(password);

            IWebElement submitBtn = _webDriver.FindElement(By.ClassName("btn-submit"));
            submitBtn.Click();

            var profiles = _webDriver.FindElements(By.ClassName("profile-name"));
            
            var profile = profiles.FirstOrDefault(p => p.Text == profileName);
            profile.Click();
        }

        public override IEnumerable<string> GetListings()
        {
            const string listingUri = "https://www.netflix.com/search";
            const string formattableQueryString = "?q={0}";

            var listings = new HashSet<string>();

            System.IO.File.WriteAllText("out.txt", string.Empty);
            for (char c = 'a'; c <= 'z'; c++)
            {)
                _webDriver.Url = listingUri + string.Format(formattableQueryString, c.ToString());

                var lastPageLength = 0;

                do
                {
                    lastPageLength = _webDriver.PageSource.Length;

                    IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                    System.Threading.Thread.Sleep(1000);
                }while(lastPageLength < _webDriver.PageSource.Length);

                var elements = _webDriver.FindElements(By.ClassName("fallback-text"))
                                         .Select(ele => ele.Text);

                System.IO.File.AppendAllText("out.txt", $"{c.ToString()} - {elements.Count()}\n");

                foreach (var element in elements)
                {
                    listings.Add(element);
                }
            }

            return listings;
        }
    }
}
