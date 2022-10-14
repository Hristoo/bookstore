using BookStore.BL.Kafka;
using Json2KafkaCache.Caches;
using Json2KafkaCache.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Json2KafkaCache
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var cache = new JsonCacheDistributor<int , BranchInfo>();
            var token = new CancellationTokenSource();

            await cache.Execute(token.Token);
        }
    }
}