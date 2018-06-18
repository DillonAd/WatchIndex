using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WatchIndex
{
    public abstract class Aggregator : IAggregator
    {
        private readonly HttpClient _httpClient;

        public Aggregator()
        {
            _httpClient = new HttpClient();
        }

        public abstract Task Authenticate(string userName, string password);

        public async Task<string> GetAsync(Uri uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}