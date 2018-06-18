using WatchIndex.Configuration;
using System;
using System.Collections.Generic;

namespace WatchIndex
{
    public class ListingScraper
    {
        private readonly IEnumerable<IAggregator> _aggregators;
        private readonly IConfig _config;

        public ListingScraper(IEnumerable<IAggregator> aggregators, IConfig config)
        {
            _aggregators = aggregators;
            _config = config;
        }

        public void Scrape()
        {
            var listings = new List<string>();
            (string userName, string password) credentials;

            foreach(var aggregator in _aggregators)
            {
                credentials = _config.GetCredentials(aggregator.ServiceKey);
                aggregator.Authenticate(credentials.userName, credentials.password);
                listings.AddRange(aggregator.GetListings());
            }

            foreach(var listing in listings)
            {
                Console.WriteLine(listing);
            }
        }
    }
}