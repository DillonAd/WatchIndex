using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WatchIndex.Configuration;

namespace WatchIndex
{
    public class ListingScraper : IListingScraper
    {
        private readonly IEnumerable<Aggregator> _aggregators;
        private readonly IConfig _config;
        private readonly ITitleStore _titleStore;

        public ListingScraper(IEnumerable<Aggregator> aggregators, IConfig config, ITitleStore titleStore)
        {
            _aggregators = aggregators;
            _config = config;
            _titleStore = titleStore;
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
                _titleStore.Add(listing);
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

                _titleStore.Dispose();
            }
        }
    }
}