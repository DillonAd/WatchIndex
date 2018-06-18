using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace WatchIndex.Configuration
{
    public class JsonConfig : IConfig
    {
        public (string userName, string password) GetCredentials(string serviceName)
        {
            Credential[] credentials;

            using(StreamReader reader = File.OpenText("credentials.json"))
            {
                var serializer = JsonSerializer.Create();
                credentials = (Credential[])serializer.Deserialize(reader, typeof(Credential[]));
            }

            var cred = credentials.FirstOrDefault(c => c.ServiceKey == serviceName);
            return (cred?.UserName, cred?.Password);
        }

        class Credential
        {
            public string ServiceKey { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}