using HtmlAgilityPack;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchIndex
{
    public class NetflixAggregator : Aggregator
    {
        public NetflixAggregator(IWebDriver webDriver) 
            : base("Netflix", webDriver) { }

        public override void Authenticate(string userName, string password, string profileName)
        {
            const string signInUrl = "https://www.netflix.com/login";

            WebDriver.Url = signInUrl;

            IWebElement userNameTb = WebDriver.FindElement(By.Id("id_userLoginId"));
            userNameTb.SendKeys(userName);

            IWebElement passwordTb = WebDriver.FindElement(By.Id("id_password"));
            passwordTb.SendKeys(password);

            IWebElement submitBtn = WebDriver.FindElement(By.ClassName("btn-submit"));
            submitBtn.Click();

            var profiles = WebDriver.FindElements(By.ClassName("profile-name"));
            
            var profile = profiles.FirstOrDefault(p => p.Text == profileName);
            profile.Click();
        }

        public override IEnumerable<string> GetListings()
        {
            const string listingUri = "https://www.netflix.com/search";
            const string formattableQueryString = "?q={0}";

            var listings = new HashSet<string>();

            foreach (var c in Alphabet)
            {
                WebDriver.Url = listingUri + string.Format(formattableQueryString, c.ToString());

                var lastPageLength = 0;

                do
                {
                    lastPageLength = WebDriver.PageSource.Length;

                    IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriver;
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                    System.Threading.Thread.Sleep(1000);

                }
                while(lastPageLength < WebDriver.PageSource.Length);

                var elements = WebDriver.FindElements(By.ClassName("fallback-text"))
                                        .Select(ele => ele.Text);

                foreach (var element in elements)
                {
                    listings.Add(element);
                }
            }

            return listings;
        }
    }
}
