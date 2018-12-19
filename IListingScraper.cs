using System;

namespace WatchIndex
{
    public interface IListingScraper : IDisposable
    {
        void Scrape();
    }
}
