using BookStore.BL.Kafka;
using Confluent.Kafka;
using Json2KafkaCache.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Json2KafkaCache.Caches
{
    public class JsonCacheDistributor<TKey, TValue> where TValue : ICacheItem<TKey>
    {
        List<TValue> _data;
        private readonly Producer<TKey, TValue> _producer;
        public JsonCacheDistributor()
        {
            var fileNames = Directory.GetFiles("Data", "*json");
            _data = new List<TValue>();

            foreach (var file in fileNames)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string json = sr.ReadToEnd();
                    var brancInfo = JsonConvert.DeserializeObject<List<TValue>>(json);

                    _data.AddRange(brancInfo);
                }
            }
            _producer = new Producer<TKey, TValue>();
        }

        public async Task Execute(CancellationToken cancellation)
        {
            foreach (var item in _data)
            {
                var msg = new Message<TKey, TValue>()
                {
                    Key = item.GetKey(),
                    Value = item
                };
                await _producer.SendMessage(msg);
            }
        }
    }
}
