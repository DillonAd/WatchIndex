using Microsoft.Extensions.DependencyInjection;

namespace WatchIndex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IAggregator, NetflixAggregator>()
                .BuildServiceProvider();       
        }
    }
}
