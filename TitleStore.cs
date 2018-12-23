using System;
using System.IO;
using System.Threading.Tasks;

namespace TitleStore
{
    public sealed class TitleStore : IDisposable
    {
        private readonly StreamWriter _writer;
        private readonly string _fileName;

        public TitleStore()
        {
            _fileName = "output.txt";

            _writer = new StreamWriter(_fileName, false);
        }

        public async Task Write(string content) =>
            await _writer.WriteLineAsync(content);

        public async void Dispose()
        {
            await _writer.FlushAsync();
            _writer.Dispose();
        }
    }
}