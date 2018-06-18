using System.Threading.Tasks;

namespace WatchIndex
{
    public class NetflixAggregator : Aggregator
    {
        public NetflixAggregator() : base() { }

        public override async Task Authenticate(string userName, string password)
        {
            //TODO Implement Authentication
        }
    }
}
