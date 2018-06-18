using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WatchIndex
{
    public abstract class Aggregator
    {
        private readonly HttpClient _httpClient;

        public Aggregator()
        {
            _httpClient = new HttpClient();
        }

        public abstract Task Authenticate(string userName, string password);

        public async Task<string> Get(Uri uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}