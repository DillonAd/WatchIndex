using System.Collections.Generic;

namespace WatchIndex
{
    public interface IAggregator
    {
        void Authenticate(string userName, string password);
        IEnumerable<string> GetListings();
    }
}