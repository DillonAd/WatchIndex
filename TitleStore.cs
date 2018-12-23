using System;
using System.IO;
using System.Threading.Tasks;

namespace WatchIndex
{
    public sealed class TitleStore : ITitleStore
    {
        private readonly StreamWriter _writer;
        private readonly string _fileName;

        public TitleStore()
        {
            _fileName = "output.txt";

            _writer = new StreamWriter(_fileName, false);
        }

        public async Task Add(string title) =>
            await _writer.WriteLineAsync(title);

        public async void Dispose()
        {
            await _writer.FlushAsync();
            _writer.Dispose();
        }
    }
}