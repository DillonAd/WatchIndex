using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WatchIndex
{
    class Program
    {
        public static void Main(string[] args)
        {
            var listings = GetAllListings().GetAwaiter().GetResult();

            foreach(var listing in listings)
            {
                Console.WriteLine(listing);
            }

        }

        private async static Task<IEnumerable<string>> GetAllListings()
        {
            IEnumerable<string> listings;

            using (var netflix = new NetflixAggregator())
            {
                listings = await netflix.GetListings();
            }

            return listings;
        }
    }
}
