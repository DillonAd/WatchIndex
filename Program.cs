using Microsoft.Extensions.DependencyInjection;
using WatchIndex.Configuration;

namespace WatchIndex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<Aggregator, NetflixAggregator>()
                .AddTransient<IConfig, JsonConfig>()
                .BuildServiceProvider();       
        }
    }
}
