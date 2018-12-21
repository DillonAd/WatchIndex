using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WatchIndex.Configuration;

namespace WatchIndex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IWebDriver>((sp) => 
                {
                    if(Environment.OSVersion.Platform == PlatformID.Unix)
                    {
                        return new ChromeDriver("/usr/lib/chromium-browser/");
                    }
                    else
                    {
                        return new ChromeDriver();
                    }
                })
                .AddSingleton<Aggregator, NetflixAggregator>()
                .AddSingleton<IConfig, JsonConfig>()
                .AddSingleton<IListingScraper, ListingScraper>()
                .BuildServiceProvider();

            var listingScraper = serviceProvider.GetService<IListingScraper>();

            try
            {
                listingScraper.Scrape();
            }
            catch(Exception ex)
            {
                Console.WriteLine(GetExceptionDetails(ex));
            }
            finally
            {
                listingScraper.Dispose();
            }
        }

        static string GetExceptionDetails(Exception ex) =>
            ex == null ? string.Empty : $"{ex.Message}\n{ex.StackTrace}\n\n{GetExceptionDetails(ex.InnerException)}";
    }
}
