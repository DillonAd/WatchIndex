using System;
using System.Threading.Tasks;

namespace WatchIndex
{
    public interface ITitleStore : IDisposable
    {
        Task Add(string title);
    }
}