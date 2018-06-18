using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WatchIndex.Configuration;

namespace WatchIndex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IWebDriver, ChromeDriver>()
                .AddTransient<Aggregator, NetflixAggregator>()
                .AddTransient<IConfig, JsonConfig>()
                .AddTransient<IListingScraper, ListingScraper>()
                .BuildServiceProvider();

            var listingScraper = serviceProvider.GetService<IListingScraper>();
            listingScraper.Scrape();
        }
    }
}
