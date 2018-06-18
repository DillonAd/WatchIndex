using System;
using System.Threading.Tasks;

namespace WatchIndex
{
    public interface IAggregator : IDisposable
    {
        Task<string> GetAsync(Uri uri);
    }
}
