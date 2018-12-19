using System;
using System.Collections.Generic;
using System.Linq;
using WatchIndex.Configuration;

namespace WatchIndex
{
    public class ListingScraper : IListingScraper
    {
        private readonly IEnumerable<Aggregator> _aggregators;
        private readonly IConfig _config;

        public ListingScraper(IEnumerable<Aggregator> aggregators, IConfig config)
        {
            _aggregators = aggregators;
            _config = config;
        }

        public void Scrape()
        {
            var listings = new List<string>();
            Credential credentials;

            foreach(var aggregator in _aggregators)
            {
                credentials = _config.GetCredentials(aggregator.ServiceKey);
                aggregator.Authenticate(credentials.UserName, credentials.Password, credentials.ProfileName);
                listings.AddRange(aggregator.GetListings());
            }

            foreach(var listing in listings.OrderBy(l => l))
            {
                Console.WriteLine(listing);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                foreach(var aggregator in _aggregators)
                {
                    aggregator.Dispose();
                }
            }
        }
    }
}