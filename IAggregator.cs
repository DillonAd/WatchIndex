using System.Collections.Generic;

namespace WatchIndex
{
    public interface IAggregator
    {
        string ServiceKey { get; }
        void Authenticate(string userName, string password);
        IEnumerable<string> GetListings();
    }
}