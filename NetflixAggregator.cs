using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchIndex
{
    public class NetflixAggregator : Aggregator
    {
        public NetflixAggregator() : base() { }

        public override async Task Authenticate(string userName, string password)
        {
            //TODO Implement Authentication
        }

        public async Task<IEnumerable<string>> GetListings()
        {
            const string listingUri = "https://www.netflix.com/search";
            const string formattableQueryString = "?q={0}";

            var listings = new List<string>();
            HtmlNodeCollection results;
            HtmlDocument htmlDoc;

            for (char c = 'a'; c <= 'z'; c++)
            {
                htmlDoc = await GetAsync(listingUri + string.Format(formattableQueryString, c.ToString()));
                results = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"title-card-0-*\"]/div/a/div/div/div");

                if (results.Any())
                {
                    foreach (var node in results)
                    {
                        listings.Add(node.InnerText);
                    }
                }
            }

            return listings;
        }
    }
}
