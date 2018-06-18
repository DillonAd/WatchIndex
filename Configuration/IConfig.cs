namespace WatchIndex.Configuration
{
    public interface IConfig
    {
        (string userName, string password) GetCredentials(string serviceName);
    }
}