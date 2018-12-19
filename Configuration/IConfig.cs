namespace WatchIndex.Configuration
{
    public interface IConfig
    {
        Credential GetCredentials(string serviceName);
    }
}