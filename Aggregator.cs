using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WatchIndex
{
    public abstract class Aggregator : IDisposable
    {
        private readonly HttpClient _httpClient;

        public Aggregator()
        {
            _httpClient = new HttpClient();
        }

        public abstract Task Authenticate(string userName, string password);

        protected async Task<HtmlDocument> GetAsync(string uri)
        {
            return await GetAsync(new Uri(uri));
        }

        protected async Task<HtmlDocument> GetAsync(Uri uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var result =  await response.Content.ReadAsStreamAsync();

            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(result);

            return htmlDoc;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}