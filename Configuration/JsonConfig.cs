using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace WatchIndex.Configuration
{
    public class JsonConfig : IConfig
    {
        public Credential GetCredentials(string serviceName)
        {
            var text = File.ReadAllText("credentials.json");
            var credentials = JsonConvert.DeserializeObject<Credential[]>(text);            

            var cred = credentials.FirstOrDefault(c => c.ServiceKey == serviceName);
            return cred;
        }
    }
}