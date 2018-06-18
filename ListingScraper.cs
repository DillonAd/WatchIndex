using System;
using System.Collections.Generic;

namespace WatchIndex
{
    public class ListingScraper
    {
        private readonly IEnumerable<IAggregator> _aggregators;

        public ListingScraper(IEnumerable<IAggregator> aggregators)
        {
            _aggregators = aggregators;
        }

        public void Scrape()
        {
            var listings = new List<string>();

            foreach(var aggregator in _aggregators)
            {
                listings.AddRange(aggregator.GetListings());
            }
            
            foreach(var listing in listings)
            {
                Console.WriteLine(listing);
            }
        }
    }
}